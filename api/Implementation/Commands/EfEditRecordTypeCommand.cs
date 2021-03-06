using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands
{
    public class EfEditRecordTypeCommand : IEditRecordTypeCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly EditRecordTypeValidator _val;
        public EfEditRecordTypeCommand(RadContext con, IMapper mapper, EditRecordTypeValidator val)
        {
            _con = con;
            _mapper = mapper;
            _val = val;
        }
        public int Id => 52;

        public string Name => "Edit Record Type Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(RecordTypeDto req)
        {
            _val.ValidateAndThrow(req);
            var r = _con.Roles.Where(x => x.IsDeleted == false && x.Id == req.Id).FirstOrDefault();

            if (r == null)
            {
                throw new InvalidEntityException(req.Id, typeof(RecordType));
            }

            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                r.Name = req.Name;
            }

            _con.SaveChanges();
        }
    }
}
