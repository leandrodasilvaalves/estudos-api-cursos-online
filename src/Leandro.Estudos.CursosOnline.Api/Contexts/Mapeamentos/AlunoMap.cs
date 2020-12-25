using Leandro.Estudos.CursosOnline.Api.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leandro.Estudos.CursosOnline.Api.Contexts.Mapeamentos
{
  public class AlunoMap : IEntityTypeConfiguration<Aluno>
  {
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nome)
            .HasColumnType("varchar(120)")
            .IsRequired();

        builder.Property(a => a.Email)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder.ToTable("Alunos");

        builder          
          .HasMany(a => a.Cursos)
          .WithMany(c => c.Alunos)
          .UsingEntity(ac => ac.ToTable("CursosAlunos"));
    }
  }
}