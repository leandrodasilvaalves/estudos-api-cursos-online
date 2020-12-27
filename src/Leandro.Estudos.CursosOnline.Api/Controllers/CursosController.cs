using System;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Leandro.Estudos.CursosOnline.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public partial class CursosController : ControllerBase
  {
    private readonly ICursoRepositorio _repositorio;
    private readonly ICursoServico _servico;

    public CursosController(ICursoRepositorio repositorio, ICursoServico servico)
    {
      _repositorio = repositorio;
      _servico = servico;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
      var cursos = await _repositorio.Listar();
      if (cursos == null) return NoContent();
      return Ok(new OkResponse(cursos));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> Get(Guid id)
    {
      var curso = await _repositorio.ObterPorId(id);
      if (curso == null) return NoContent();
      return Ok(new OkResponse(curso));
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Curso curso)
    {
      await _servico.Incluir(curso);
      return Ok(new OkResponse("Curso cadastrado com sucesso", curso));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] Curso curso)
    {
      if (id != curso.Id)
        return BadRequest("O id da rota precisa ser igual ao id do curso");

      var cursoBanco = await _repositorio.ObterPorId(id);
      if (cursoBanco == null)
        return NotFound("Curso não localizado na base dados");

      cursoBanco.AtualizarPropriedades(curso);
      await _servico.Editar(cursoBanco);
      return Ok(new OkResponse("Curso atualizado com sucesso", cursoBanco));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
      var curso = await _repositorio.ObterPorId(id);
      if (curso == null)
        return NotFound("Curso não localizado na base dados");

      await _servico.Excluir(id);
      return Ok(new OkResponse("Curso excluído com sucesso"));
    }
  }
}