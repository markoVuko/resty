using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Implementation.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CategoryDto>
    {
        public CreateCategoryValidator(RadContext con)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name must not be empty!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name).Must(x =>
                    {
                        return !con.Categories.Any(c => c.Name == x);
                    }).WithMessage("This category name is taken!");
                });
        }
    }
}
