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
    public class EfCreateSupplierCommand : ICreateSupplierCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly CreateSupplierValidator _val;
        public EfCreateSupplierCommand(RadContext con, IMapper mapper, CreateSupplierValidator val)
        {
            _val = val;
            _mapper = mapper;
            _con = con;
        }
        public int Id => 36;

        public string Name => "Create Supplier Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(SupplierDto req)
        {
            _val.ValidateAndThrow(req);

            Supplier s = _mapper.Map<Supplier>(req);
            s.CreatedAt = DateTime.UtcNow;
            s.IsActive = true;
            s.IsDeleted = false;

            _con.Suppliers.Add(s);
            _con.SaveChanges();
        }
    }
}
