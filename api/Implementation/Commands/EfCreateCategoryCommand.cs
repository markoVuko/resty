using Application.Commands;
using Application.DTO;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class EfCreateCategoryCommand : ICreateCategoryCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly CreateCategoryValidator _val;
        public EfCreateCategoryCommand(RadContext con, IMapper mapper, CreateCategoryValidator val)
        {
            _val = val;
            _mapper = mapper;
            _con = con;
        }
        public int Id => 54;

        public string Name => "Create Category Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(CategoryDto req)
        {
            _val.ValidateAndThrow(req);
            Category r = _mapper.Map<Category>(req);
            r.CreatedAt = DateTime.UtcNow;
            r.IsActive = true;
            r.IsDeleted = false;

            _con.Categories.Add(r);
            _con.SaveChanges();
        }
    }
}
