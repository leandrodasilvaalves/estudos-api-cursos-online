using System;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Contexts;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Leandro.Estudos.CursosOnline.Api.Repositorios
{
  public class CursoRepositorio : RepositorioBase<Curso>, ICursoRepositorio
  {
    public CursoRepositorio(CursoContext db) : base(db) { }

    public async Task<Curso> ObterCursoComAlunos(Guid id)
    {
      return await _db.Cursos
                    .AsNoTracking()
                    .Include(c => c.Alunos)
                    .FirstOrDefaultAsync(c => c.Id == id);
    }
  }
}