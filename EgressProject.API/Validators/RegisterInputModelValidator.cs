using EgressProject.API.Models.InputModel;
using FluentValidation;

namespace EgressProject.API.Validators
{
    public class RegisterInputModelValidator : AbstractValidator<RegisterInputModel>
    {
        public RegisterInputModelValidator()
        {
            RuleFor(r => r.Email)
                .EmailAddress().WithMessage("Email inválido")
                .NotEmpty().WithMessage("O campo Email não preenchido");

            RuleFor(r => r.Name)
                .MaximumLength(100).WithMessage("É permitido até 100 caracteres")
                .NotEmpty().WithMessage("O campo Name não preenchido");
            
            RuleFor(r => r.DocType)
                .Must(dt => dt.Equals("cpf") || dt.Equals("mat")).WithMessage("DocType aceita somente os valores: 'mat' ou 'cpf'")
                .NotEmpty().WithMessage("O campo DocType não preenchido");
            
            RuleFor(r => r.DocNumber)
                .MaximumLength(20).WithMessage("É permitido até 20 caracteres")
                .NotEmpty().WithMessage("O campo DocNumber não preenchido");
            
            RuleFor(r => r.Password)
                .MinimumLength(8).WithMessage("Mínimo de 8 caracteres")
                .MaximumLength(30).WithMessage("Máximo de 30 caracteres")
                .NotEmpty().WithMessage("O campo Password não preenchido");

            RuleFor(r => r.PasswordRepeat)
                .Equal(r => r.Password).WithMessage("O PasswordRepeat deve ser igual ao Password")
                .MinimumLength(8).WithMessage("Mínimo de 8 caracteres")
                .MaximumLength(30).WithMessage("Máximo de 30 caracteres")
                .NotEmpty().WithMessage("O campo PasswordRepeat não preenchido");
        }
    }
}