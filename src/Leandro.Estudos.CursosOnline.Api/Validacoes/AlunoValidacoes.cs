using FluentValidation;
using Leandro.Estudos.CursosOnline.Api.Entidades;

namespace Leandro.Estudos.CursosOnline.Api.Validacoes
{
  public class AlunoValidacoes : AbstractValidator<Aluno>
  {
    public AlunoValidacoes()
    {
      RuleFor(a => a.Nome)
          .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
          .Length(2, 120).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

      RuleFor(a => a.Email)
          .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
          .MaximumLength(150).WithMessage("O campo {PropertyName} precisa ter no máximo {MaxLength} caracteres e foi fornecido {TotalLength}")
          .EmailAddress().WithMessage("O campo {PropertyName} precisa ser um e-mail válido");
    }
  }
}