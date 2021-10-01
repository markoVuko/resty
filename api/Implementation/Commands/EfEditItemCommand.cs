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
    public class EfEditItemCommand : IEditItemCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly EditItemValidator _val;
        public EfEditItemCommand(RadContext con, IMapper mapper, EditItemValidator val)
        {
            _con = con;
            _mapper = mapper;
            _val = val;
        }
        public int Id => 10;

        public string Name => "Edit Item Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(ItemDto req)
        {
            _val.ValidateAndThrow(req);

            var item = _con.Items.Include(x => x.Supplier)
                .Include(x => x.CategoryItems)
                .ThenInclude(x => x.Category)
                .Where(x => x.IsDeleted == false && x.Id == req.Id)
                .FirstOrDefault();

            if (item == null)
            {
                throw new InvalidEntityException(req.Id, typeof(Item));
            }

            if (req.Quantity > 0)
            {
                item.Quantity = req.Quantity;
            }
            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                item.Name = req.Name;
            }
            if (req.SupplierId > 0)
            {
                item.SupplierId = req.SupplierId;
            }

            _con.SaveChanges();
        }
    }
}
