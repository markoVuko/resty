using Application;
using Application.DTO;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetScheduleQuery : IGetScheduleQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly IAppActor _actor;
        public EfGetScheduleQuery(RadContext con, IMapper mapper, IAppActor actor)
        {
            _con = con;
            _mapper = mapper;
            _actor = actor;
        }
        public int Id => 6;

        public string Name => "Get Schedule Query";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public CreateScheduleDto Execute(int req)
        {
            var s = _con.Schedules.Include(x => x.WorkType)
                .Include(x => x.User)
                .Where(x => x.IsDeleted == false && x.Id == req)
                .FirstOrDefault();
            if (_actor.RoleId==1||_actor.Id==s.UserId)
            {
                if (s == null)
                {
                    throw new InvalidEntityException(req, typeof(Schedule));
                }
                return _mapper.Map<CreateScheduleDto>(s);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

        }
    }
}
