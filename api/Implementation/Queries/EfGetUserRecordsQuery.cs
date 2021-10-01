using Application.DTO;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetUserRecordsQuery : IGetUserRecordsQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetUserRecordsQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 33;

        public string Name => "Get User Records Query";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public PagedResponse<UserRecordDto> Execute(UserRecordSearchDto req)
        {
            var q = _con.UserRecords.Include(x=>x.User)
                .Include(x=>x.RecordType)
                .Where(x => x.IsDeleted == false).AsQueryable();

            if (!string.IsNullOrWhiteSpace(req.Keyword))
            {
                q = q.Where(x => x.User.FirstName.ToLower().Contains(req.Keyword.ToLower())
                || x.User.LastName.ToLower().Contains(req.Keyword.ToLower())
                || x.User.Email.ToLower().Contains(req.Keyword.ToLower())
                || x.RecordType.Name.ToLower().Contains(req.Keyword.ToLower()));
            }

            if (req.UserId > 0)
            {
                q = q.Where(x => x.User.Id == req.UserId);
            }

            if (req.RecordTypeId > 0)
            {
                q = q.Where(x => x.RecordType.Id == req.RecordTypeId);
            }

            var offset = req.PerPage * (req.Page - 1);
            if (offset < 0)
            {
                offset = 0;
                req.Page = 1;
            }

            var res = new PagedResponse<UserRecordDto>
            {
                PerPage = req.PerPage,
                TotalItems = q.Count(),
                CurrentPage = req.Page,
                Items = q.Skip(offset).Take(req.PerPage).Select(x => _mapper.Map<UserRecordDto>(x)).ToList()
            };

            return res;
        }
    }
}
