using System;
using System.Collections.Generic;

namespace Leandro.Estudos.CursosOnline.Api.Entidades
{
  public class Curso : EntidadeBase
    {
        public string Nome { get; set; }  

        public IEnumerable<Aluno> Alunos { get; internal set; }

        public void AtualizarPropriedades(Curso curso){
          this.Nome = curso.Nome;
        }
    }
}