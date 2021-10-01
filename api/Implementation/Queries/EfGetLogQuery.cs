using Application.DTO;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetLogQuery : IGetLogQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetLogQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 23;

        public string Name => "Get Log Query";

        public List<int> AllowedRoles => new List<int> { 1 };

        public LogDto Execute(int req)
        {
            var log = _con.ActionLogs.Where(x => x.Id == req && x.IsDeleted == false).FirstOrDefault();
            if (log == null)
            {
                throw new InvalidEntityException(req, typeof(ActionLog));
            }

            return _mapper.Map<LogDto>(log);
        }
    }
}
