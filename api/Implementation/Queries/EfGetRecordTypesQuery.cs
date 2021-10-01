using Application.DTO;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetRecordTypesQuery : IGetRecordTypesQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetRecordTypesQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 46;

        public string Name => "Get Record Types Query";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public PagedResponse<RecordTypeDto> Execute(RecordTypeSearchDto req)
        {
            var q = _con.RecordTypes
                .Where(x => x.IsDeleted == false).AsQueryable();

            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                q = q.Where(x => x.Name.ToLower().Contains(req.Name.ToLower()));
            }

            if (req.Page == -1)
            {
                req.Page = 1;
                req.PerPage = q.Count();
            }


            var offset = req.PerPage * (req.Page - 1);

            var res = new PagedResponse<RecordTypeDto>
            {
                PerPage = req.PerPage,
                TotalItems = q.Count(),
                CurrentPage = req.Page,
                Items = q.Skip(offset).Take(req.PerPage).Select(x => _mapper.Map<RecordTypeDto>(x)).ToList()
            };

            return res;
        }
    }
}
