using System;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Contexts;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Leandro.Estudos.CursosOnline.Api.Repositorios
{
  public class AlunoRepositorio : RepositorioBase<Aluno>, IAlunoRepositorio
  {
    public AlunoRepositorio(CursoContext db) : base(db) {}

    public async Task<Aluno> ObterAlunoComCursos(Guid id)
    {
        return await _db.Alunos
                        .AsNoTracking()
                        .Include(a => a.Cursos)
                        .FirstOrDefaultAsync(a => a.Id == id);
    }
  }
}