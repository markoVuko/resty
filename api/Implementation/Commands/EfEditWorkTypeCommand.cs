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
    public class EfEditWorkTypeCommand : IEditWorkTypeComand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly EditWorkTypeValidator _val;
        public EfEditWorkTypeCommand(RadContext con, IMapper mapper, EditWorkTypeValidator val)
        {
            _con = con;
            _mapper = mapper;
            _val = val;
        }
        public int Id => 59;

        public string Name => "Edit Work Type Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(WorkTypeDto req)
        {
            _val.ValidateAndThrow(req);
            var r = _con.WorkTypes.Where(x => x.IsDeleted == false && x.Id == req.Id).FirstOrDefault();

            if (r == null)
            {
                throw new InvalidEntityException(req.Id, typeof(WorkType));
            }

            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                r.Name = req.Name;
            }

            _con.SaveChanges();
        }
    }
}
