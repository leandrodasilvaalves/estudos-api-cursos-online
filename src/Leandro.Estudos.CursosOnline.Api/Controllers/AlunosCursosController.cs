using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Leandro.Estudos.CursosOnline.Api.Controllers
{
  public partial class AlunosController : ControllerBase
  {

    [HttpGet("{id:guid}/cursos")]
    public async Task<ActionResult<IEnumerable<Aluno>>> Cursos(Guid id)
    {
      var alunos = await _repositorio.ObterAlunoComCursos(id);
      if (alunos == null) return NoContent();
      return Ok(new OkResponse(alunos));
    }

    [HttpPut("{id:guid}/matricular/{cursoId:guid}")]
    public async Task<ActionResult> Matricular(Guid id, Guid cursoId)
    {
      var aluno = await _repositorio.ObterPorId(id);
      if (aluno == null)
        return NotFound("Aluno não localizado na base de dados");

      var curso = await _cursoRepositorio.ObterPorId(cursoId);
      if (curso == null)
        return NotFound("Curso não localizado na base dados");

      aluno.Matricular(curso);
      await _servico.Editar(aluno);
      return Ok(new OkResponse());
    }
  }
}