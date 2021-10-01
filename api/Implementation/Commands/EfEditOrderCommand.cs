using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands
{
    public class EfEditOrderCommand : IEditOrderCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly CreateOrderValidator _val;
        public EfEditOrderCommand(RadContext con, IMapper mapper, CreateOrderValidator val)
        {
            _con = con;
            _mapper = mapper;
            _val = val;
        }
        public int Id => 15;

        public string Name => "Edit Order Command";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public void Execute(OrderDto req)
        {
            _val.ValidateAndThrow(req);

            var order = _con.Orders.Include(x => x.OrderLines)
                .Where(x => x.IsDeleted == false && x.Id == req.Id)
                .FirstOrDefault();
            if (order == null)
            {
                throw new InvalidEntityException(req.Id, typeof(Order));
            }
            if (req.UserId>=0&&req.UserId!=order.UserId)
            {
                order.UserId = req.UserId;
            }
            _con.SaveChanges();
        }
    }
}
