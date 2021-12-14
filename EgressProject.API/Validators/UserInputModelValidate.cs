using EgressProject.API.Models.Enums;
using EgressProject.API.Models.InputModel;
using FluentValidation;

namespace EgressProject.API.Validators
{
    public class UserInputModelValidate : AbstractValidator<UserInputModel>
    {
        public UserInputModelValidate()
        {
            RuleFor(u => u.Id)
                .NotNull().WithMessage("Id não inserido");
                
            RuleFor(u => u.Email)
                .EmailAddress().WithMessage("Email inválido");
                
            RuleFor(u => u.Password)
                .MinimumLength(8).WithMessage("Mínimo de 8 caracteres")
                .MaximumLength(30).WithMessage("Máximo de 30 caracteres");
            
            RuleFor(u => u.Role)
                .IsEnumName(typeof(Role)).WithMessage("Não é um Role válido");
        }
    }
}