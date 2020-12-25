using System;
using System.Collections.Generic;

namespace Leandro.Estudos.CursosOnline.Api.Entidades
{
  public class Aluno : EntidadeBase
  {
    public string Nome { get; private set; }

    public string Email { get; set; }
    public IEnumerable<Curso> Cursos { get; set; }
  }
}