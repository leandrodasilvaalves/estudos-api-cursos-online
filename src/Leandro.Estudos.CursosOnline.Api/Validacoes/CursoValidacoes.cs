using FluentValidation;
using Leandro.Estudos.CursosOnline.Api.Entidades;

namespace Leandro.Estudos.CursosOnline.Api.Validacoes
{
  public class CursoValidacoes : AbstractValidator<Curso>
  {
    public CursoValidacoes()
    {
      RuleFor(c => c.Nome)
          .NotEmpty().WithMessage("O campo {PropertyName} precisa sre fornecido")
          .Length(2, 80).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
  }
}