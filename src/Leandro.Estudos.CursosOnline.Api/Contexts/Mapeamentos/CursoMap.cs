using Leandro.Estudos.CursosOnline.Api.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leandro.Estudos.CursosOnline.Api.Contexts.Mapeamentos
{
  public class CursoMap : IEntityTypeConfiguration<Curso>
  {
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .HasColumnType("varchar(80)")
            .IsRequired();

        builder.ToTable("Cursos");
    }
  }
}