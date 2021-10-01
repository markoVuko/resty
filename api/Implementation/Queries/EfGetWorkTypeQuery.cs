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
    public class EfGetWorkTypeQuery : IGetWorkTypeQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetWorkTypeQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 60;

        public string Name => "Get Work Type Query";

        public List<int> AllowedRoles => new List<int> { 1 };

        public WorkTypeDto Execute(int req)
        {
            var r = _con.WorkTypes.Where(x => x.IsDeleted == false && x.Id == req).FirstOrDefault();
            if (r == null)
            {
                throw new InvalidEntityException(req, typeof(WorkType));
            }

            return _mapper.Map<WorkTypeDto>(r);
        }
    }
}
