using Application.DTO;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetLogsQuery : IGetLogsQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public int Id => 24;

        public string Name => "Get Logs Query";

        public List<int> AllowedRoles => new List<int> { 1 };

        public PagedResponse<LogDto> Execute(LogSearchDto req)
        {
            var q = _con.ActionLogs.Where(x => x.IsDeleted == false).AsQueryable();

            if (!string.IsNullOrWhiteSpace(req.ActionName))
            {
                q = q.Where(x => x.ActionName.ToLower().Contains(req.ActionName.ToLower()));
            }
            if (req.ActorId >= 0)
            {
                q = q.Where(x => x.ActorId == req.ActorId);
            }
            if (req.LogId >= 0)
            {
                q = q.Where(x => x.Id == req.LogId);
            }

            var offset = req.PerPage * (req.Page - 1);

            var res = new PagedResponse<LogDto>
            {
                PerPage = req.PerPage,
                TotalItems = q.Count(),
                CurrentPage = req.Page,
                Items = q.Skip(offset).Take(req.PerPage).Select(x => _mapper.Map<LogDto>(x)).ToList()
            };

            return res;
        }
    }
}
