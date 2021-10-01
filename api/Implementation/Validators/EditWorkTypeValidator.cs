using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Implementation.Validators
{
    public class EditWorkTypeValidator : AbstractValidator<WorkTypeDto>
    {
        public EditWorkTypeValidator(RadContext con)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Work type name must not be empty!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name).Must((a,x) =>
                    {
                        return !con.WorkTypes.Any(c => c.Name == x && c.Id != a.Id);
                    }).WithMessage("This work type name is taken!");
                });

            RuleFor(x => x.HourlyRate).GreaterThan(0).WithMessage("The hourly rate must be more than 0!");
        }
    }
}
