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
    public class EfCreateUserRecordCommand : ICreateUserRecordCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly CreateUserRecordValidator _val;
        public EfCreateUserRecordCommand(RadContext con, IMapper mapper, CreateUserRecordValidator val)
        {
            _con = con;
            _mapper = mapper;
            _val = val;
        }
        public int Id => 29;

        public string Name => "Create User Record Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(UserRecordDto req)
        {
            _val.ValidateAndThrow(req);

            UserRecord ur = _mapper.Map<UserRecord>(req);
            ur.CreatedAt = DateTime.UtcNow;
            ur.IsDeleted = false;
            ur.IsActive = true;

            _con.UserRecords.Add(ur);
            _con.SaveChanges();
        }
    }
}
