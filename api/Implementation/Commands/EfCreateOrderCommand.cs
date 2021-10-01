using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands
{
    public class EfCreateOrderCommand : ICreateOrderCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly CreateOrderValidator _val;
        public EfCreateOrderCommand(RadContext con, IMapper mapper, CreateOrderValidator val)
        {
            _con = con;
            _mapper = mapper;
            _val = val;
        }
        public int Id => 13;

        public string Name => "Create Order Command";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public void Execute(OrderDto req)
        {
            _val.ValidateAndThrow(req);

            foreach(var oL in req.OrderLines)
            {
                Item item = _con.Items.Where(x => x.IsDeleted == false && x.Id == oL.ItemId).FirstOrDefault();
                if (item.Quantity == 0 || item.Quantity < oL.Quantity)
                {
                    throw new Exception("There isn't enough of " + item.Name + " to make this order!");
                }
                item.Quantity = item.Quantity - oL.Quantity;
            }

            Order order = _mapper.Map<Order>(req);
            order.IsDeleted = false;
            order.IsActive = true;
            order.CreatedAt = DateTime.UtcNow;

            order.OrderLines = req.OrderLines.Select(x => new OrderLine
            {
                ItemId = x.ItemId,
                ItemName = x.ItemName,
                Quantity = x.Quantity,
                Price = x.Price,
                IsDeleted = false,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            _con.Orders.Add(order);
            _con.SaveChanges();
        }
    }
}
