using System;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Leandro.Estudos.CursosOnline.Api.Validacoes
{
  public class UserClaimValidations : AbstractValidator<IdentityUserClaim<Guid>>
  {
    public UserClaimValidations()
    {
      RuleFor(x => x.UserId)
          .NotEmpty().WithErrorCode("O campo {PropertyName} precisa ser fornecido");

      RuleFor(x => x.ClaimType)
          .NotEmpty().WithErrorCode("O campo {PropertyName} precisa ser fornecido");

      RuleFor(x => x.ClaimValue)
          .NotEmpty().WithErrorCode("O campo {PropertyName} precisa ser fornecido");
    }
  }
}