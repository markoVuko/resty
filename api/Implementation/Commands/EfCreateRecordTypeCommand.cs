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
    public class EfCreateRecordTypeCommand : ICreateRecordTypeCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly CreateRecordTypeValidator _val;
        public EfCreateRecordTypeCommand(RadContext con, IMapper mapper, CreateRecordTypeValidator val)
        {
            _con = con;
            _val = val;
            _mapper = mapper;
        }
        public int Id => 51;

        public string Name => "Create Work Type Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(RecordTypeDto req)
        {
            _val.ValidateAndThrow(req);

            RecordType r = _mapper.Map<RecordType>(req);

            r.CreatedAt = DateTime.UtcNow;
            r.IsDeleted = false;
            r.IsActive = true;

            _con.RecordTypes.Add(r);
            _con.SaveChanges();
        }
    }
}
