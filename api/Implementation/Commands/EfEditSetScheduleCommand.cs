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
    public class EfEditSetScheduleCommand : IEditSetScheduleCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly CreateScheduleValidator _val;
        public EfEditSetScheduleCommand(RadContext con, IMapper mapper, CreateScheduleValidator val)
        {
            _con = con;
            _val = val;
            _mapper = mapper;
        }

        public int Id => 2;

        public string Name => "Edit Set Schedule Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(CreateScheduleDto req)
        {
            _val.ValidateAndThrow(req);
            var s = _con.Schedules.Where(x => x.Id == req.Id&&x.IsDeleted==false).FirstOrDefault();
            if (s == null)
            {
                throw new InvalidEntityException(req.Id, typeof(Schedule));
            }
            s.DateStart = req.DateStart;
            s.DateEnd = req.DateEnd;
            s.ModifiedAt = DateTime.UtcNow;
            _con.SaveChanges();
        }
    }
}
