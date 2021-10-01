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
    public class EfCreateWorkTypeCommand : ICreateWorkTypeCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly CreateWorkTypeValidator _val;
        public EfCreateWorkTypeCommand(RadContext con, IMapper mapper, CreateWorkTypeValidator val)
        {
            _val = val;
            _mapper = mapper;
            _con = con;
        }
        public int Id => 58;

        public string Name => "Create Work Type Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(WorkTypeDto req)
        {
            _val.ValidateAndThrow(req);
            WorkType r = _mapper.Map<WorkType>(req);
            r.CreatedAt = DateTime.UtcNow;
            r.IsActive = true;
            r.IsDeleted = false;

            _con.WorkTypes.Add(r);
            _con.SaveChanges();
        }
    }
}
