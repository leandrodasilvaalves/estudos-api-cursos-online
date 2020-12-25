using System;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace Leandro.Estudos.CursosOnline.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CursosController : ControllerBase
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
      var cursos = _repositorio.Listar();
      if (cursos == null) return NoContent();
      return Ok(await cursos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> Get(Guid id)
    {
      var curso = await _repositorio.ObterPorId(id);
      if (curso == null) return NoContent();
      return Ok(curso);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Curso curso)
    {
      try
      {
          await _servico.Incluir(curso);
          return Ok(curso);
      }
      catch (Exception ex)
      {
          return BadRequest(ex.Message);
      }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id, [FromBody]Curso curso)
    {
        if(id != curso.Id) 
            return BadRequest("O id da rota precisa ser igual ao ida da entidade");

        var cursoBanco = await _repositorio.ObterPorId(id);
        if(cursoBanco == null)
            return NotFound("Curso não localizado na base dados");

        await _servico.Editar(curso);
        return Ok(curso);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var curso = await _repositorio.ObterPorId(id);
        if(curso == null)
            return NotFound("Curso não localizado na base dados");

        await _servico.Excluir(id);
        return Ok(curso);
    }
  }
}