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
    public class EfGetRolesQuery : IGetRolesQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public int Id => 22;

        public string Name => "Get Role";

        public List<int> AllowedRoles => new List<int> { 1 };

        public PagedResponse<RoleDto> Execute(RoleSearchDto req)
        {
            var q = _con.Roles.Where(x => x.IsDeleted == false).AsQueryable();
            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                q = q.Where(x => x.Name.ToLower().Contains(req.Name.ToLower()));
            }

            var offset = req.PerPage * (req.Page - 1);

            var res = new PagedResponse<RoleDto>
            {
                PerPage = req.PerPage,
                TotalItems = q.Count(),
                CurrentPage = req.Page,
                Items = q.Skip(offset).Take(req.PerPage).Select(x => _mapper.Map<RoleDto>(x)).ToList()
            };

            return res;
        }
    }
}
