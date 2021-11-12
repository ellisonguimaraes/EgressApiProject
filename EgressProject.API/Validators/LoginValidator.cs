using EgressProject.API.Models.Utils;
using FluentValidation;

namespace EgressProject.API.Validators
{
    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Email)
                .EmailAddress().WithMessage("É necessário que seja um email")
                .NotEmpty().WithMessage("O campo email precisar ser preenchido");
            
            RuleFor(l => l.Password)
                .MinimumLength(8).WithMessage("Mínimo de 8 caracteres")
                .MaximumLength(30).WithMessage("Máximo de 30 caracteres")
                .NotEmpty().WithMessage("O campo senha precisar ser preenchido");
        }        
    }
}