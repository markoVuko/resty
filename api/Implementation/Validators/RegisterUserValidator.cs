using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(RadContext con)
        {
            RuleFor(x => x.DateOfBirth)
                .NotNull()
                .WithMessage("Date of birth can not be null!");
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(30)
                .WithMessage("Password can not be less than 8 or more than 30 characters!");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30)
                .WithMessage("First name must be at least 3 and no more than 30 characters!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30)
                .WithMessage("Last name must be at least 3 and no more than 30 characters!");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Please enter a valid email!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Email).Must((user, email) =>
                    {
                        return !con.Users.Any(u => u.Email == email);
                    })
                    .WithMessage("This email is already taken by another user!");
                });
        }
    }
}
