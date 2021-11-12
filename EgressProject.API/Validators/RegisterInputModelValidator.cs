using EgressProject.API.Models.InputModel;
using FluentValidation;

namespace EgressProject.API.Validators
{
    public class RegisterInputModelValidator : AbstractValidator<RegisterInputModel>
    {
        public RegisterInputModelValidator()
        {
            RuleFor(r => r.Email)
                .EmailAddress().WithMessage("É necessário que seja um email")
                .NotEmpty().WithMessage("O campo email precisar ser preenchido");
            
            RuleFor(r => r.Password)
                .MinimumLength(8).WithMessage("Mínimo de 8 caracteres")
                .MaximumLength(30).WithMessage("Máximo de 30 caracteres")
                .NotEmpty().WithMessage("O campo senha precisar ser preenchido");
        }
    }
}