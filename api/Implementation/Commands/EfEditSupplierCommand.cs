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
    public class EfEditSupplierCommand : IEditSupplierCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly EditSupplierValidator _val;
        public EfEditSupplierCommand(RadContext con, IMapper mapper, EditSupplierValidator val)
        {
            _con = con;
            _mapper = mapper;
            _val = val;
        }
        public int Id => 41;

        public string Name => "Edit Supplier Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(SupplierDto req)
        {
            _val.ValidateAndThrow(req);
            var r = _con.Suppliers.Where(x => x.IsDeleted == false && x.Id == req.Id).FirstOrDefault();

            if (r == null)
            {
                throw new InvalidEntityException(req.Id, typeof(Supplier));
            }

            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                r.Name = req.Name;
            }

            if (!string.IsNullOrWhiteSpace(req.Address))
            {
                r.Address = req.Address;
            }

            if (!string.IsNullOrWhiteSpace(req.Mail))
            {
                r.Mail = req.Mail;
            }

            if (!string.IsNullOrWhiteSpace(req.Phone))
            {
                r.Phone = req.Phone;
            }

            if (!string.IsNullOrWhiteSpace(req.City))
            {
                r.City = req.City;
            }

            _con.SaveChanges();
        }
    }
}
