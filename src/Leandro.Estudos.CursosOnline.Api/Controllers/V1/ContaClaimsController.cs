using System;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Leandro.Estudos.CursosOnline.Api.Controllers.V1
{
  [ApiController]
  //[Authorize]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}")]
  public class ContaClaimsController : ControllerBase
  {
    private readonly IContaServico _contaServico;
    private readonly INotificador _notificador;

    public ContaClaimsController(IContaServico contaServico, INotificador notificador)
    {
      _contaServico = contaServico;
      _notificador = notificador;
    }

    [HttpGet("usuarios-claims")]
    public async Task<ActionResult> ObterUsuariosComClaims()
    {
      var usuariosClaims = await _contaServico.ObterUsuariosComClaims();
      return Ok(new OkResponse(usuariosClaims));
    }

    [HttpPost("usuarios-claims")]
    public async Task<ActionResult> ObterUsuariosComClaims(IdentityUserClaim<Guid> userClaim)
    {
      if ((await _contaServico.ObterPorUsuarioId(userClaim.UserId)) == null)
        return NotFound(new NotFoundResponse("Usuário não localizado na base de dados", userClaim));

      if (await _contaServico.CadastrarClaim(userClaim))
        return Ok(new OkResponse("Claim cadastrada com sucesso", userClaim));

      return BadRequest(
        new BadRequestResponse(
          "Não foi possível cadastrar a claim",
          _notificador.ObterNotificacoes(),
          userClaim));
    }
  }
}