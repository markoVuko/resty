using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class EditSupplierValidator : AbstractValidator<SupplierDto>
    {
        public EditSupplierValidator(RadContext con)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Supplier name can not be null!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name).Must((sup,x) =>
                    {
                        return !con.Suppliers.Any(r => r.Id != sup.Id && r.Name == x);
                    }).WithMessage("Supplier name is already taken.");
                });
        }
    }
}
