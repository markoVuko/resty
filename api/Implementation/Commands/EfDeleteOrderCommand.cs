using Application.Commands;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands
{
    public class EfDeleteOrderCommand : IDeleteOrderCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfDeleteOrderCommand(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 14;

        public string Name => "Delete Order Command";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public void Execute(int req)
        {
            var order = _con.Orders.Include(x => x.OrderLines)
                .Where(x => x.IsDeleted == false && x.Id == req)
                .FirstOrDefault();
            if (order == null)
            {
                throw new InvalidEntityException(req, typeof(Order));
            }

            foreach(var oL in order.OrderLines)
            {
                var item = _con.Items.Where(x => x.IsDeleted == false && x.Id == oL.ItemId).FirstOrDefault();
                if (item == null)
                {
                    continue;
                }
                item.Quantity = item.Quantity + oL.Quantity;
            }

            order.IsDeleted = true;
            order.ModifiedAt = DateTime.UtcNow;
            order.IsActive = false;

            _con.SaveChanges();
        }
    }
}
