using System;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Leandro.Estudos.CursosOnline.Api.Controllers
{
  public partial class CursosController : ControllerBase
  {
    [HttpGet("{id:guid}/alunos")]
    public async Task<ActionResult<Curso>> Alunos(Guid id)
    {
      var curso = await _repositorio.ObterCursoComAlunos(id);
      if (curso == null) return NotFound("Curso não localizado na base de dados");
      return Ok(curso);
    }
  }
}