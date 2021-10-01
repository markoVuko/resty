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
    public class EfEditCategoryCommand : IEditCategoryCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly EditCategoryValidator _val;
        public EfEditCategoryCommand(RadContext con, IMapper mapper, EditCategoryValidator val)
        {
            _con = con;
            _mapper = mapper;
            _val = val;
        }
        public int Id => 55;

        public string Name => "Edit Category Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(CategoryDto req)
        {
            _val.ValidateAndThrow(req);
            var r = _con.Categories.Where(x => x.IsDeleted == false && x.Id == req.Id).FirstOrDefault();

            if (r == null)
            {
                throw new InvalidEntityException(req.Id, typeof(Category));
            }

            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                r.Name = req.Name;
            }

            _con.SaveChanges();
        }
    }
}
