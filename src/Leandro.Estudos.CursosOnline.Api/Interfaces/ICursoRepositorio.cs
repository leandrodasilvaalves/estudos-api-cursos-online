using System;
using Leandro.Estudos.CursosOnline.Api.Entidades;

namespace Leandro.Estudos.CursosOnline.Api.Interfaces
{
    public interface ICursoRepositorio : IRepositorioBase<Curso>
    {
         Curso ObterCursoComAlunos(Guid id);
    }
}