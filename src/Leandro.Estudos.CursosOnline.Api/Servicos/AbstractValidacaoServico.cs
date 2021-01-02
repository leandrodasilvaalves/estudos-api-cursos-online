namespace Leandro.Estudos.CursosOnline.Api.Servicos
{
  using FluentValidation;
  using FluentValidation.Results;
  using Leandro.Estudos.CursosOnline.Api.Interfaces;
  using Leandro.Estudos.CursosOnline.Api.Notificacoes;
  public abstract class AbstractValidacaoServico<T> where T : class
  {
    private readonly INotificador _notificador;

    protected AbstractValidacaoServico(INotificador notificador)
    {
      _notificador = notificador;
    }

    protected bool ExecutarValidacao<TV>(TV validacao, T entitdade) where TV : AbstractValidator<T>
    {
      var validator = validacao.Validate(entitdade);
      Notificar(validator);
      return validator.IsValid;
    }

    protected void Notificar(ValidationResult validator)
    {
      foreach (var error in validator.Errors)
        Notificar(error.ErrorMessage);
    }

    protected void Notificar(string mensagem)
    {
      _notificador.Handle(new Notificacao(mensagem));
    }
  }
}