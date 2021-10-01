using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class EditRoleValidator : AbstractValidator<RoleDto>
    {
        public EditRoleValidator(RadContext con)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Role name can not be null!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name).Must((a,b) =>
                    {
                        return !con.Roles.Any(r => r.Id != a.Id && r.Name == b);
                    }).WithMessage("Role name is already taken.");
                });
        }
    }
}
