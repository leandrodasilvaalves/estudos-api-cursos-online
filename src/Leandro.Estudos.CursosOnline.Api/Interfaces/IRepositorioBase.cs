using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;

namespace Leandro.Estudos.CursosOnline.Api.Interfaces
{
  public interface IRepositorioBase<T> where T : EntidadeBase
  {
    Task<IEnumerable<T>> Listar();
    Task<T> ObterPorId(Guid id);
    Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate);
    Task Incluir(T entidade);
    Task Editar(T entidade);
    Task Excluir(Guid id);
    Task<int> Salvar();
  }
}