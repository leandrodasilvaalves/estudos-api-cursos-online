using System;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;

namespace Leandro.Estudos.CursosOnline.Api.Interfaces
{
  public interface IAlunoRepositorio : IRepositorioBase<Aluno>
  {
    Task<Aluno> ObterAlunoComCursos(Guid id);
  }
}