using System;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;

namespace Leandro.Estudos.CursosOnline.Api.Servicos
{
  public abstract class ServicoBase<T> : IServicoBase<T> where T : EntidadeBase
  {
    public IRepositorioBase<T> _repositorio { get; }

    public ServicoBase(IRepositorioBase<T> repositorio)
    {
        _repositorio = repositorio;
    }    

    public async Task Incluir(T entidade)
    {
      entidade
        .GerarNovoId()
        .Ativar()
        .DefinirDataCadastro()
        .DefinirUltimaAtualizacao();

      await _repositorio.Incluir(entidade);
    }

    public async Task Editar(T entidade)
    {
      entidade.DefinirUltimaAtualizacao();
      await _repositorio.Editar(entidade);
    }

    public async Task Excluir(Guid id)
    {
      await _repositorio.Excluir(id);
    }
  }
}