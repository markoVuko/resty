using Application.DTO;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetRoleQuery : IGetRoleQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetRoleQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 21;

        public string Name => "Get Role Query";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public RoleDto Execute(int req)
        {
            var r = _con.Roles.Where(x => x.IsDeleted == false && x.Id == req).FirstOrDefault();
            if (r == null)
            {
                throw new InvalidEntityException(req, typeof(Role));
            }

            return _mapper.Map<RoleDto>(r);
        }
    }
}
