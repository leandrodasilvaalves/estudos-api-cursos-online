using System;
using System.Collections.Generic;

namespace Leandro.Estudos.CursosOnline.Api.Entidades
{
  public class Aluno : EntidadeBase
  {
    public Aluno()
    {
      Cursos = new List<Curso>();
    }
    public string Nome { get; set; }

    public string Email { get; set; }
    public string Imagem { get; set; }
    public ICollection<Curso> Cursos { get; internal set; }

    public void Matricular(Curso curso)
    {
      Cursos.Add(curso);
    }

    internal void MergearDados(Aluno aluno)
    {
      Nome = aluno.Nome;
      Email = aluno.Email;
      Imagem = string.IsNullOrEmpty(aluno.Imagem) ? Imagem : aluno.Imagem;
    }
  }
}