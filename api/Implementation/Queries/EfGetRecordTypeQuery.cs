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
    public class EfGetRecordTypeQuery : IGetRecordTypeQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetRecordTypeQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 45;

        public string Name => "Get Record Type Query";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public RecordTypeDto Execute(int req)
        {
            var item = _con.RecordTypes
                .Where(x => x.IsDeleted == false && x.Id == req)
                .FirstOrDefault();

            if (item == null)
            {
                throw new InvalidEntityException(req, typeof(RecordType));
            }

            return _mapper.Map<RecordTypeDto>(item);
        }
    }
}
