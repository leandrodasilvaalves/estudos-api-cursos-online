using System;
using Leandro.Estudos.CursosOnline.Api.Entidades;

namespace Leandro.Estudos.CursosOnline.Api.Interfaces
{
  public interface IAlunoRepositorio : IRepositorioBase<Aluno>
  {
    Aluno ObterAlunoComCursos(Guid id);
  }
}