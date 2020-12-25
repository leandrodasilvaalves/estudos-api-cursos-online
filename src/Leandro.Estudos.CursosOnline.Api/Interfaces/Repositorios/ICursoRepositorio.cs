using System;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;

namespace Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios
{
    public interface ICursoRepositorio : IRepositorioBase<Curso>
    {
         Task<Curso> ObterCursoComAlunos(Guid id);
    }
}