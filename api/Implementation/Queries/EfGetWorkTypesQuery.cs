using Application.DTO;
using Application.Exceptions;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetWorkTypesQuery : IGetWorkTypesQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetWorkTypesQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 50;

        public string Name => "Get Work Types Query";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public PagedResponse<WorkTypeDto> Execute(WorkTypeSearchDto req)
        {
            var q = _con.WorkTypes.Where(x => x.IsDeleted == false).AsQueryable();
            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                q = q.Where(x => x.Name.ToLower().Contains(req.Name.ToLower()));
            }


            var offset = req.PerPage * (req.Page - 1);

            var res = new PagedResponse<WorkTypeDto>
            {
                PerPage = req.PerPage,
                TotalItems = q.Count(),
                CurrentPage = req.Page,
                Items = q.Skip(offset).Take(req.PerPage).Select(x => _mapper.Map<WorkTypeDto>(x)).ToList()
            };

            return res;
        }
    }
}
