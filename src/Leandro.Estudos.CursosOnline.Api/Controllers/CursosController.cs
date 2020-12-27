using System;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
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
    private readonly INotificador _notificador;

    public CursosController(ICursoRepositorio repositorio,
                            ICursoServico servico,
                            INotificador notificador)
    {
      _repositorio = repositorio;
      _servico = servico;
      _notificador = notificador;
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
      if (await _servico.Incluir(curso))
        return Ok(new OkResponse("Curso cadastrado com sucesso", curso));

      var mensagemErro = "Ocorreram um ou mais erros ao tentar cadastrar o curso";
      return BadRequest(new BadRequestResponse(mensagemErro, _notificador.ObterNotificacoes(), curso));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] Curso curso)
    {
      if (id != curso.Id)
        return BadRequest(new BadRequestResponse("O id da rota precisa ser igual ao id do curso"));

      var cursoBanco = await _repositorio.ObterPorId(id);
      if (cursoBanco == null)
        return NotFound(new NotFoundResponse("Curso não localizado na base dados"));

      cursoBanco.AtualizarPropriedades(curso);
      if (await _servico.Editar(cursoBanco))
        return Ok(new OkResponse("Curso atualizado com sucesso", cursoBanco));

      var mensagemErro = "Ocorreram um ou mais erros ao tentar editar o curso";
      return BadRequest(new BadRequestResponse(mensagemErro, _notificador.ObterNotificacoes(), curso));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
      var curso = await _repositorio.ObterPorId(id);
      if (curso == null)
        return NotFound(new NotFoundResponse("Curso não localizado na base dados"));

      await _servico.Excluir(id);
      return Ok(new OkResponse("Curso excluído com sucesso"));
    }
  }
}