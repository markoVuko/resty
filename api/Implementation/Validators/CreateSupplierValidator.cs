using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateSupplierValidator : AbstractValidator<SupplierDto>
    {
        public CreateSupplierValidator(RadContext con) {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Supplier name can not be null!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name).Must(x =>
                    {
                        return !con.Suppliers.Any(r => r.Name.ToLower().Contains(x.ToLower()));
                    }).WithMessage("Supplier name is already taken.");
                });
        }

    }
}
