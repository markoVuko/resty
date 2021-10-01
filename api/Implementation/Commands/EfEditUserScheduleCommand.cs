using Application;
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
    public class EfEditUserScheduleCommand : IEditSetScheduleCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly EditSetScheduleValidator _val;
        private readonly IAppActor _actor;
        public EfEditUserScheduleCommand(IAppActor actor, RadContext con, IMapper mapper, EditSetScheduleValidator val)
        {
            _con = con;
            _val = val;
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 3;

        public string Name => "Edit User Schedule Command";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public void Execute(CreateScheduleDto req)
        {
            _val.ValidateAndThrow(req);
            if (_actor.RoleId == 1 || _actor.Id == req.UserId)
            {
                var s = _con.Schedules.Where(x => x.IsDeleted == false && x.Id == req.Id).FirstOrDefault();
                if (s == null)
                {
                    throw new InvalidEntityException(req.Id, typeof(Schedule));
                }
                s.ModifiedAt = DateTime.UtcNow;
                if (_actor.RoleId == 1)
                {
                    s.DateStart = req.DateStart;
                    s.DateEnd = req.DateEnd;
                    s.DateBegun = req.DateBegun;
                    s.DateFin = req.DateFin;
                    s.WorkTypeId = req.WorkTypeId;
                }
                else
                {
                    if (req.ClockedOut)
                    {
                        s.DateFin = req.DateFin;
                    } else
                    {
                        s.DateBegun = req.DateBegun;
                    }
                }
                _con.SaveChanges();
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
