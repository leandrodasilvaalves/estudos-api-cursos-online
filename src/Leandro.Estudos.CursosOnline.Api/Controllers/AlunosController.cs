using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace Leandro.Estudos.CursosOnline.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public partial class AlunosController : ControllerBase
  {
      private readonly IAlunoRepositorio _repositorio;
      private readonly IAlunoServico _servico;
      private readonly ICursoRepositorio _cursoRepositorio;

    public AlunosController(IAlunoRepositorio repositorio, IAlunoServico servico, ICursoRepositorio cursoRepositorio)
    {
      _repositorio = repositorio;
      _servico = servico;
      _cursoRepositorio = cursoRepositorio;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Aluno>>> Get()
    {
        var alunos = await _repositorio.Listar();
        if(alunos == null) return NoContent();
        return Ok(alunos);
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Aluno>> Get(Guid id)
    {
        var aluno = await _repositorio.ObterPorId(id);
        if(aluno == null) return NoContent();
        return Ok(aluno);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Aluno aluno)
    {
        await _servico.Incluir(aluno);
        return Ok(aluno);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] Aluno aluno)
    {
        if(id != aluno.Id)
            return BadRequest("O id da rota precisa ser igual ao id do aluno");

        var alunoBanco = await _repositorio.ObterPorId(id);
        if(alunoBanco == null)
            return NotFound("Aluno não localizado na base de dados");

        await _servico.Editar(aluno);
        return Ok(aluno);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var aluno = await _repositorio.ObterPorId(id);
        if(aluno == null)
            return NotFound("Aluno não localizado na base de dados");

        await _servico.Excluir(id);
        return Ok();
    }
  }
}