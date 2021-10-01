using Application.Commands;
using Application.DTO;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class EfCreateRoleCommand : ICreateRoleCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly CreateRoleValidator _val;
        public EfCreateRoleCommand(RadContext con, IMapper mapper, CreateRoleValidator val) 
        {
            _val = val;
            _mapper = mapper;
            _con = con;
        }
        public int Id => 18;

        public string Name => "Create Role Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(RoleDto req)
        {
            _val.ValidateAndThrow(req);
            Role r = _mapper.Map<Role>(req);
            r.CreatedAt = DateTime.UtcNow;
            r.IsActive = true;
            r.IsDeleted = false;

            _con.Roles.Add(r);
            _con.SaveChanges();
        }
    }
}
