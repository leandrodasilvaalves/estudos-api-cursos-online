using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Leandro.Estudos.CursosOnline.Api.Controllers.V1
{
  [Authorize]
  [ApiController]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  public partial class AlunosController : ControllerBase
  {
    private readonly IAlunoRepositorio _repositorio;
    private readonly IAlunoServico _servico;
    private readonly ICursoRepositorio _cursoRepositorio;
    private readonly INotificador _notificador;

    public AlunosController(IAlunoRepositorio repositorio,
                            IAlunoServico servico,
                            ICursoRepositorio cursoRepositorio,
                            INotificador notificador)
    {
      _repositorio = repositorio;
      _servico = servico;
      _cursoRepositorio = cursoRepositorio;
      _notificador = notificador;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Aluno>>> Get()
    {
      var alunos = await _repositorio.Listar();
      if (alunos == null) return NoContent();
      return Ok(new OkResponse(alunos));
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Aluno>> Get(Guid id)
    {
      var aluno = await _repositorio.ObterPorId(id);
      if (aluno == null) return NoContent();
      return Ok(new OkResponse(aluno));
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Aluno aluno)
    {
      if (await _servico.Incluir(aluno))
        return Ok(new OkResponse("Aluno cadastrado com sucesso", aluno));

      var mensagemErro = "Ocorreram um ou mais erros ao tentar cadastrar o aluno";
      return BadRequest(new BadRequestResponse(mensagemErro, _notificador.ObterNotificacoes(), aluno));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] Aluno aluno)
    {
      if (id != aluno.Id)
        return BadRequest(new BadRequestResponse("O id da rota precisa ser igual ao id do aluno"));

      var alunoBanco = await _repositorio.ObterPorId(id);
      if (alunoBanco == null)
        return NotFound(new NotFoundResponse("Aluno não localizado na base de dados"));

      if (await _servico.Editar(aluno))
        return Ok(new OkResponse("Aluno atualizado com sucesso", aluno));

      var mensagemErro = "Ocorreram um ou mais erros ao tentar editar o aluno";
      return BadRequest(new BadRequestResponse(mensagemErro, _notificador.ObterNotificacoes(), aluno));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
      var aluno = await _repositorio.ObterPorId(id);
      if (aluno == null)
        return NotFound(new NotFoundResponse("Aluno não localizado na base de dados"));

      await _servico.Excluir(id);
      return Ok(new OkResponse("Aluno excluído com sucesso"));
    }
  }
}