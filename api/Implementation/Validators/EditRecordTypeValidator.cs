using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class EditRecordTypeValidator : AbstractValidator<RecordTypeDto>
    {
        public EditRecordTypeValidator(RadContext con)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Record type name can not be empty!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name).Must((a, b) =>
                    {
                        return !con.RecordTypes.Any(r => r.Name == b && r.Id != a.Id);
                    }).WithMessage("This record type name is taken!");
                });
        }
    }
}
