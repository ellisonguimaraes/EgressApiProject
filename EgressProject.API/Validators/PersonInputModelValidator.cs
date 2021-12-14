using EgressProject.API.Models.InputModel;
using FluentValidation;

namespace EgressProject.API.Validators
{
    public class PersonInputModelValidator : AbstractValidator<PersonInputModel>
    {
        public PersonInputModelValidator()
        {
            RuleFor(p => p.Name)
                .MaximumLength(150).WithMessage("Máximo de 150 caracteres")
                .NotEmpty().WithMessage("Campo Name não preenchido");

            RuleFor(p => p.Cpf)
                .MaximumLength(20).WithMessage("Máximo de 20 caracteres")
                .NotEmpty().WithMessage("Campo Cpf não preenchido");
            
            RuleFor(p => p.PhoneNumber)
                .MaximumLength(20).WithMessage("Máximo de 20 caracteres");
            
            RuleFor(p => p.PhoneNumber2)
                .MaximumLength(20).WithMessage("Máximo de 20 caracteres");
            
            RuleFor(p => p.City)
                .MaximumLength(50).WithMessage("Máximo de 50 caracteres");
            
            RuleFor(p => p.State)
                .MaximumLength(50).WithMessage("Máximo de 50 caracteres");
            
            RuleFor(p => p.Country)
                .MaximumLength(50).WithMessage("Máximo de 50 caracteres");
        }
    }
}