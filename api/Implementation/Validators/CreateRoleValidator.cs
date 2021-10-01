using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateRoleValidator : AbstractValidator<RoleDto>
    {
        public CreateRoleValidator(RadContext con)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Role name can not be null!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name).Must(x =>
                    {
                        return !con.Roles.Any(r => r.Name == x);
                    }).WithMessage("Role name is already taken.");
                });
        }
    }
}
