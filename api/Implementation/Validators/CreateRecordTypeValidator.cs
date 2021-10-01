using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateRecordTypeValidator : AbstractValidator<RecordTypeDto>
    {
        public CreateRecordTypeValidator(RadContext con)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Record type name can not be empty!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name).Must(x =>
                    {
                        return !con.RecordTypes.Any(r => r.Name == x);
                    }).WithMessage("This record type name is taken!");
                });
        }
    }
}
