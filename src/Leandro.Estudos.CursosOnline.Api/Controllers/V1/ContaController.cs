using System;
using System.Linq;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Extensoes;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Leandro.Estudos.CursosOnline.Api.Controllers.V1
{

  [ApiController]
  [Authorize]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}")]
  public class ContaController : ControllerBase
  {
    private readonly IJwtServico _jwtServico;
    private readonly IContaServico _contaServico;
    private readonly INotificador _notificador;

    public ContaController(IJwtServico jwtServico,
                           IContaServico contaServico,
                           INotificador notificador)
    {
      _jwtServico = jwtServico;
      _contaServico = contaServico;
      _notificador = notificador;
    }

    [AllowAnonymous]
    [HttpPost("registrar")]
    public async Task<ActionResult> Registrar([FromBody] ContaRegistroModel model)
    {
      if (await _contaServico.Registrar(model))
      {
        await _contaServico.Logar(new ContaLoginModel { Email = model.Email, Senha = model.Senha });
        return Ok(new OkAuthResponse("Usuário registrado com sucesso", token: await _jwtServico.GerarToken(model.Email)));
      }
      return BadRequest(new BadRequestResponse("Não foi possível registrar o usuário", _notificador.ObterNotificacoes(), model));
    }

    [AllowAnonymous]
    [HttpPost("entrar")]
    public async Task<ActionResult> Logar([FromBody] ContaLoginModel model)
    {
      if (await _contaServico.Logar(model))
        return Ok(new OkAuthResponse("Usuário logado com sucesso", token: await _jwtServico.GerarToken(model.Email)));

      return NotFound(new NotFoundResponse("Usuário ou senha inválidos"));
    }

    [HttpPut("trocar-senha/{id:guid}")]
    public async Task<ActionResult> TrocarSenha(Guid id, [FromBody] ContaTrocaSenhaModel model)
    {
      if (id != model.Id)
        return BadRequest(new BadRequestResponse("O id da rota precisa ser igual ao id do usuário"));

      if (ModelState.IsInvalid())
        return BadRequest(new BadRequestResponse("Não foi possível atualizar a senha", ModelState, model));

      if (model.SenhaAtual == model.NovaSenha)
        return BadRequest(new BadRequestResponse("A nova senha precisa ser diferente da senha antiga"));

      var usuario = await _contaServico.ObterPorUsuarioId(id);
      if (usuario == null)
        return NotFound(new NotFoundResponse("Usuário não localizado na base de dados"));

      if (await _contaServico.TrocarSenha(usuario, model))
        return Ok(new OkResponse("Senha atualizada com sucesso"));

      return BadRequest(new BadRequestResponse("Não foi possível trocar a senha", _notificador.ObterNotificacoes(), model));
    }
  }
}