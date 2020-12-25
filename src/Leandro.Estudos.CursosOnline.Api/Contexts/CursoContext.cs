using Leandro.Estudos.CursosOnline.Api.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Leandro.Estudos.CursosOnline.Api.Contexts
{
  public class CursoContext : DbContext
  {
    public CursoContext(DbContextOptions<CursoContext> options) 
      : base(options) {}

    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Aluno> Alunos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(CursoContext).Assembly);
      base.OnModelCreating(modelBuilder);
    }
  }
}