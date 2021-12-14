using EgressProject.API.Models.InputModel;
using FluentValidation;

namespace EgressProject.API.Validators
{
    public class TokenInputModelValidator : AbstractValidator<TokenInputModel>
    {
        public TokenInputModelValidator()
        {
            RuleFor(t => t.AccessToken)
                .NotEmpty().WithMessage("O campo AccessToken está vazio");
                
            RuleFor(t => t.RefreshToken)
                .NotEmpty().WithMessage("O campo RefreshToken está vazio");
        }
    }
}