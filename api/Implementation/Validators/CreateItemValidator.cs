using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateItemValidator : AbstractValidator<ItemDto>
    {
        public CreateItemValidator(RadContext con)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Item name can not be empty!").DependentRules(() =>
                {
                    RuleFor(x => x.Name).Must(x =>
                    {
                        return !con.Items.Any(r => r.Name.ToLower().Contains(x.ToLower()));
                    }).WithMessage("Item name is already taken.");
                });
            RuleFor(x => x.SupplierId).Must(id =>
            {
                return con.Suppliers.Any(s => s.Id == id);
            }).WithMessage("Supplier id must belong to a real supplier!");
            RuleFor(x => x.Categories)
                .NotNull()
                .WithMessage("Categories can not be null!")
                .DependentRules(() => {
                    RuleFor(x => x.Categories).Must(cats =>
                    {
                        var distC = cats.Select(x => x.Name).Distinct().Count();
                        return distC == cats.Count();
                    }).WithMessage("You can not have duplicate categories!");
                    RuleForEach(x => x.Categories).Must((item, cat) =>
                    {
                        return con.Categories.Any(c => c.Id == cat.Id);
                    }).WithMessage("Category ids must belong to real categories!");
                });
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price can not be zero/negative number!");
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity can not be zero/negative number!")
                .DependentRules(()=>
                {
                    RuleFor(x => x.MinQuantity).GreaterThan(0)
                    .WithMessage("Minimum quantity can not be zero/negative number!");
                });
        }
    }
}
