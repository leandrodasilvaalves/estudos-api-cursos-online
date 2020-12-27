using System;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Notificacoes;

namespace Leandro.Estudos.CursosOnline.Api.Servicos
{
  public abstract class ServicoBase<T> : IServicoBase<T> where T : EntidadeBase
  {
    private readonly IRepositorioBase<T> _repositorio;
    private readonly INotificador _notificador;

    public ServicoBase(IRepositorioBase<T> repositorio, INotificador notificador)
    {
      _repositorio = repositorio;
      _notificador = notificador;
    }

    public abstract Task<bool> Incluir(T entidade);

    public abstract Task<bool> Editar(T entidade);

    public async Task Excluir(Guid id)
    {
      await _repositorio.Excluir(id);
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