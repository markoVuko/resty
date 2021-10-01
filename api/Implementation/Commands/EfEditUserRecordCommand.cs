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
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Implementation.Commands
{
    public class EfEditUserRecordCommand : IEditUserRecordCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly CreateUserRecordValidator _val;
        public EfEditUserRecordCommand(RadContext con, IMapper mapper, CreateUserRecordValidator val)
        {
            _con = con;
            _mapper = mapper;
            _val = val;
        }
        public int Id => 30;

        public string Name => "Edit User Record Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(UserRecordDto req)
        {
            _val.ValidateAndThrow(req);
            var r = _con.UserRecords.Where(x => x.IsDeleted == false && x.Id == req.Id).FirstOrDefault();

            if (r == null)
            {
                throw new InvalidEntityException(req.Id, typeof(UserRecord));
            }

            if (!string.IsNullOrWhiteSpace(req.Comment))
            {
                r.Comment = req.Comment;
            }

            if (req.UserId >= 0&&req.UserId!=r.UserId)
            {
                r.UserId = req.UserId;
            }


            if (req.RecordTypeId >= 0 && req.RecordTypeId != r.RecordTypeId)
            {
                r.RecordTypeId = req.RecordTypeId;
            }

            _con.SaveChanges();
        }
    }
}
