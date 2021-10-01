using Application.Commands;
using Application.DTO;
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
    public class EfCreateItemCommand : ICreateItemCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly CreateItemValidator _val;
        public EfCreateItemCommand(RadContext con, IMapper mapper, CreateItemValidator val)
        {
            _con = con;
            _val = val;
            _mapper = mapper;
        }
        public int Id => 8;

        public string Name => "Create Item Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(ItemDto req)
        {
            _val.ValidateAndThrow(req);

            Item item = _mapper.Map<Item>(req);
            item.CategoryItems = req.Categories.Select(x => new CategoryItem
            {
                CategoryId = x.Id,
                IsDeleted = false,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            item.CreatedAt = DateTime.UtcNow;
            item.IsDeleted = false;
            item.IsActive = true;

            _con.Items.Add(item);
            _con.SaveChanges();
        }
    }
}
