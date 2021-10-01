using Application;
using Application.DTO;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetSchedulesQuery : IGetSchedulesQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly IAppActor _actor;
        public EfGetSchedulesQuery(RadContext con, IMapper mapper, IAppActor actor)
        {
            _con = con;
            _mapper = mapper;
            _actor = actor;
        }
        public int Id => 7;

        public string Name => "Get Schedules Command";

        public List<int> AllowedRoles => new List<int> { 0, 1, 2 };

        public PagedResponse<CreateScheduleDto> Execute(ScheduleSearchDto req)
        {
            var q = _con.Schedules.Include(x => x.User)
                .Include(x => x.WorkType)
                .Where(x=>x.IsDeleted==false)
                .AsQueryable();
            if (_actor.RoleId != 1)
            {
                q = q.Where(x => x.UserId == _actor.Id);
            }
            switch (req.Status)
            {
                case 0:
                    q = q.Where(x => x.DateBegun == DateTime.MinValue);
                    break;
                case 1:
                    q = q.Where(x => x.DateBegun != DateTime.MinValue && x.DateFin == DateTime.MinValue);
                    break;
                case 2:
                    q = q.Where(x => x.DateFin != DateTime.MinValue && x.DateBegun != DateTime.MinValue);
                    break;
                default: break;
            }
            if (!string.IsNullOrWhiteSpace(req.Keyword))
            {
                q = q.Where(x => x.BossFullName.ToLower().Contains(req.Keyword.ToLower())
                  || x.User.FirstName.ToLower().Contains(req.Keyword.ToLower())
                  || x.User.LastName.ToLower().Contains(req.Keyword.ToLower()));
            }
            if (req.WorkTypeId>0) 
            {
                q = q.Where(x => x.WorkTypeId == req.WorkTypeId);
            }
            if (req.UserId>0)
            {
                q = q.Where(x => x.UserId == req.UserId);
            }
            if(req.DateFrom > DateTime.MinValue)
            {
                q = q.Where(x => x.DateStart >= req.DateFrom);
            }
            if (req.DateTo > DateTime.MinValue)
            {
                q = q.Where(x => x.DateStart <= req.DateTo);
            }

            if (req.Page == -1)
            {
                req.Page = 1;
                req.PerPage = q.Count();
            }

            var offset = req.PerPage * (req.Page - 1);

            var res = new PagedResponse<CreateScheduleDto>
            {
                PerPage = req.PerPage,
                TotalItems = q.Count(),
                CurrentPage = req.Page,
                Items = q.Skip(offset).Take(req.PerPage).Select(x => _mapper.Map<CreateScheduleDto>(x)).ToList()
            };

            return res;

        }
    }
}
