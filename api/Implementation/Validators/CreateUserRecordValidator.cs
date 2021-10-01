using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateUserRecordValidator : AbstractValidator<UserRecordDto>
    {
        public CreateUserRecordValidator(RadContext con)
        {
            RuleFor(x => x.UserId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("User id can not be less than 0!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.UserId).Must(id =>
                    {
                        return con.Users.Any(u => u.Id == id);
                    }).WithMessage("User id must belong to a real user!");
                });

            RuleFor(x => x.Comment)
                .NotEmpty()
                .WithMessage("Comment can not be empty!");

            RuleFor(x=>x.RecordTypeId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Record type id can not be less than 0!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.RecordTypeId).Must(id =>
                    {
                        return con.RecordTypes.Any(u => u.Id == id);
                    }).WithMessage("Record type id must belong to a real record type!");
                });
        }
    }
}
