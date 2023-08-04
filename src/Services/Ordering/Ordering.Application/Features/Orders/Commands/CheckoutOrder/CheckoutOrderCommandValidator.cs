using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator: AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");

            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{EmailAddress} must not exceed 50 characters.");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is required.")
                .GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero.");

            //RuleFor(p => p.CardName)
            //    .NotEmpty().WithMessage("{CardName} is required.")
            //    .NotNull()
            //    .MaximumLength(50).WithMessage("{CardName} must not exceed 50 characters.");

            //RuleFor(p => p.CardNumber)
            //    .NotEmpty().WithMessage("{CardNumber} is required.")
            //    .NotNull();

            //RuleFor(p => p.Expiration)
            //    .NotEmpty().WithMessage("{Expiration} is required.")
            //    .NotNull();

            //RuleFor(p => p.CVV)
            //    .NotEmpty().WithMessage("{CVV} is required.")
            //    .NotNull();

            //RuleFor(p => p.PaymentMethod)
            //    .NotEmpty().WithMessage("{PaymentMethod} is required.")
            //    .NotNull();
        }
    }
}
