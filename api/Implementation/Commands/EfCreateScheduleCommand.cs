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
    public class EfCreateScheduleCommand : ICreateScheduleCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly CreateScheduleValidator _val;
        public EfCreateScheduleCommand(RadContext con, IMapper mapper, CreateScheduleValidator val)
        {
            _con = con;
            _val = val;
            _mapper = mapper;
        }

        public int Id => 1;

        public string Name => "Create Schedule Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(CreateScheduleDto req)
        {
            _val.ValidateAndThrow(req);
            Schedule s = _mapper.Map<Schedule>(req);
            s.IsActive = true;
            s.IsDeleted = false;
            s.CreatedAt = DateTime.UtcNow;

            _con.Schedules.Add(s);
            _con.SaveChanges();
        }
    }
}
