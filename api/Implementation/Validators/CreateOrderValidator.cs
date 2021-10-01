using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateOrderValidator : AbstractValidator<OrderDto>
    {
        public CreateOrderValidator(RadContext con)
        {
            RuleFor(x => x.UserId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("User id can not be less than zero!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.UserId).Must(id =>
                    {
                        return con.Users.Any(u => u.Id == id);
                    }).WithMessage("User id must belong to a real user!");
                });
            RuleFor(x => x.TableNumber)
                .GreaterThan(0)
                .WithMessage("Table number must be greater than zero!");
            //RuleFor(x => x.EmployeeFullName)
            //    .MinimumLength(10)
            //    .MaximumLength(60)
            //    .WithMessage("Employee's full name can not be less than 10 or more than 60 characters!");
            RuleFor(x => x.OrderLines)
                .NotNull()
                .WithMessage("Order lines can not be null!")
                .DependentRules(() =>
                {
                    RuleForEach(x => x.OrderLines).SetValidator(new CreateOrderLineValidator(con));
                });
        }
    }

    public class CreateOrderLineValidator : AbstractValidator<OrderLineDto>
    {
        public CreateOrderLineValidator(RadContext con)
        {
            RuleFor(x => x.ItemId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Item id can not be less than zero!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.ItemId).Must(id =>
                    {
                        return con.Items.Any(it => it.Id == id);
                    }).WithMessage("Item id must belong to a real item!");
                });
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be more than zero!");
            RuleFor(x=>x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be more than zero!");
            RuleFor(x => x.ItemName)
                .NotEmpty()
                .WithMessage("Item name can not be empty!");
        }
    }
}
