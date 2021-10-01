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
    public class EfGetUsersQuery : IGetUsersQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetUsersQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 28;

        public string Name => "GetUsersQuery";

        public List<int> AllowedRoles => new List<int> { 1 };

        public PagedResponse<UserDto> Execute(UserSearchDto req)
        {
            var q = _con.Users.Include(x=>x.Role)
                .Where(x => x.IsDeleted == false)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(req.Keyword))
            {
                q = q.Where(x => x.FirstName.ToLower().Contains(req.Keyword.ToLower())
                || x.LastName.ToLower().Contains(req.Keyword.ToLower())
                || x.Email.ToLower().Contains(req.Keyword.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(req.Address))
            {
                q = q.Where(x => x.Address.ToLower().Contains(req.Address.ToLower()));
            }

            if (req.DateFrom > DateTime.MinValue)
            {
                q = q.Where(x => x.DateOfBirth >= req.DateFrom);
            }

            if (req.DateTo > DateTime.MinValue)
            {
                q = q.Where(x => x.DateOfBirth <= req.DateTo);
            }

            if (req.Page == -1)
            {
                req.Page = 1;
                req.PerPage = q.Count();
            }

            var offset = req.PerPage * (req.Page - 1);

            var res = new PagedResponse<UserDto>
            {
                PerPage = req.PerPage,
                TotalItems = q.Count(),
                CurrentPage = req.Page,
                Items = q.Skip(offset).Take(req.PerPage).Select(x => _mapper.Map<UserDto>(x)).ToList()
            };

            return res;
        }
    }
}
