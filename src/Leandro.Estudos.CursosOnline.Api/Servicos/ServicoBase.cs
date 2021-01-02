using System;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;

namespace Leandro.Estudos.CursosOnline.Api.Servicos
{
  public abstract class ServicoBase<T> : AbstractValidacaoServico<T>, IServicoBase<T> where T : EntidadeBase
  {
    private readonly IRepositorioBase<T> _repositorio;
    private readonly INotificador _notificador;

    public ServicoBase(IRepositorioBase<T> repositorio, INotificador notificador) : base(notificador)
    {
      _repositorio = repositorio;
    }

    public abstract Task<bool> Incluir(T entidade);

    public abstract Task<bool> Editar(T entidade);

    public async Task Excluir(Guid id)
    {
      await _repositorio.Excluir(id);
    }
  }
}