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
    public ICollection<Curso> Cursos { get; internal set; }

    public void Matricular(Curso curso){
      Cursos.Add(curso);
    }
  }
}