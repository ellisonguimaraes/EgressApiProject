using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EgressProject.API.Models.InputModel;
using FluentValidation;

namespace EgressProject.API.Validators
{
    public class TokenInputModelValidator : AbstractValidator<TokenInputModel>
    {
        public TokenInputModelValidator()
        {
            RuleFor(t => t.AccessToken)
                .NotEmpty().WithMessage("O campo do token de acesso está vazio");
                
            RuleFor(t => t.RefreshToken)
                .NotEmpty().WithMessage("O campo do refresh token está vazio");
        }
    }
}