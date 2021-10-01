using Application.DTO;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetUserQuery : IGetUserQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetUserQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 27;

        public string Name => "Get User Query";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public UserDto Execute(int req)
        {
            var r = _con.Users.Include(x=>x.Role)
                .Where(x => x.IsDeleted == false && x.Id == req)
                .FirstOrDefault();
            if (r == null)
            {
                throw new InvalidEntityException(req, typeof(User));
            }

            return _mapper.Map<UserDto>(r);
        }
    }
}
