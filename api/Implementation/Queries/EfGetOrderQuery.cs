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
    public class EfGetOrderQuery : IGetOrderQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetOrderQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 16;

        public string Name => "Get Order Query";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public OrderDto Execute(int req)
        {
            var order = _con.Orders.Include(x => x.OrderLines)
                .Include(x => x.User)
                .Where(x => x.IsDeleted == false && x.Id == req)
                .FirstOrDefault();

            if (order == null)
            {
                throw new InvalidEntityException(req, typeof(Order));
            }

            return _mapper.Map<OrderDto>(order);
        }
    }
}
