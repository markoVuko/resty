using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateScheduleValidator : AbstractValidator<CreateScheduleDto>
    {
        public CreateScheduleValidator(RadContext con)
        {
            RuleFor(x => x.DateStart)
                .NotNull()
                .WithMessage("Start of schedule can not be null!");

            RuleFor(x => x.DateEnd)
                .NotNull()
                .WithMessage("End of schedule can not be null!");
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("User Id can not be less than 1!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.UserId).Must(id =>
                    {
                        return con.Users.Any(u => u.Id == id);
                    }).WithMessage("User Id must belong to a real user!");
                });
            RuleFor(x => x.WorkTypeId)
                .GreaterThan(0)
                .WithMessage("Work Type Id can not be less than 1!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.WorkTypeId).Must(id =>
                    {
                        return con.WorkTypes.Any(u => u.Id == id);
                    }).WithMessage("Work Type Id must belong to a real user!");
                });
        }
    }
}
