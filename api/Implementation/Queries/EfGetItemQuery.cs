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
    public class EfGetItemQuery : IGetItemQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetItemQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 11;

        public string Name => "Get Item Query";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public ItemDto Execute(int req)
        {
            var item = _con.Items.Include(x => x.Supplier)
                .Include(x => x.CategoryItems)
                .ThenInclude(x => x.Category)
                .Where(x => x.IsDeleted == false && x.Id == req)
                .FirstOrDefault();

            if (item == null)
            {
                throw new InvalidEntityException(req, typeof(Item));
            }

            return _mapper.Map<ItemDto>(item);
        }
    }
}
