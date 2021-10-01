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
    public class EfGetUserRecordQuery : IGetUserRecordQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetUserRecordQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 32;

        public string Name => "Get User Record Query";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public UserRecordDto Execute(int req)
        {
            var ur = _con.UserRecords.Include(x => x.User)
                .Include(x => x.RecordType)
                .Where(x => x.IsDeleted == false && x.Id == req)
                .FirstOrDefault();

            if (ur == null)
            {
                throw new InvalidEntityException(req, typeof(UserRecord));
            }

            return _mapper.Map<UserRecordDto>(ur);
        }
    }
}
