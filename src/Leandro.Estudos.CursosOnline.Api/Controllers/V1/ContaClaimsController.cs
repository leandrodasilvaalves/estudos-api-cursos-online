using System;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Leandro.Estudos.CursosOnline.Api.Extensoes.CustomAuthorization;

namespace Leandro.Estudos.CursosOnline.Api.Controllers.V1
{
  [ApiController]
  [Authorize]
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

    [ClaimsAuthorize("Claims", "Ler")]
    [HttpGet("usuarios-claims")]
    public async Task<ActionResult> ObterUsuariosComClaims()
    {
      var usuariosClaims = await _contaServico.ObterUsuariosComClaims();
      return Ok(new OkResponse(usuariosClaims));
    }

    [ClaimsAuthorize("Claims", "Inc")]
    [HttpPost("usuarios-claims")]
    public async Task<ActionResult> IncluirClaimsParaUsuario([FromBody] IdentityUserClaim<Guid> userClaim)
    {
      if ((await _contaServico.ObterPorUsuarioId(userClaim.UserId)) == null)
        return NotFound(new NotFoundResponse("Usuário não localizado na base de dados", userClaim));

      if (await _contaServico.CadastrarClaimParaUsuario(userClaim))
        return Ok(new OkResponse("Claim cadastrada com sucesso", userClaim));

      return BadRequest(
        new BadRequestResponse(
          "Não foi possível cadastrar a claim",
          _notificador.ObterNotificacoes(),
          userClaim));
    }

    [ClaimsAuthorize("Claims", "Edit")]
    [HttpPut("usuarios-claims/{idClaim:int}")]
    public async Task<ActionResult> AtualizarClaimsParaUsuario(int idClaim, [FromBody] IdentityUserClaim<Guid> userClaim)
    {
      if (idClaim != userClaim.Id)
        return BadRequest(new BadRequestResponse("O id da rota precisa ser igual ao id da claim"));

      if ((await _contaServico.ObterClaimPorId(userClaim.Id)) == null)
        return NotFound(new NotFoundResponse("Claim não localizada na base de dados", userClaim));

      if (await _contaServico.AtualizarClaimParaUsuario(userClaim))
        return Ok(new OkResponse("Claim atualizada com sucesso", userClaim));

      return BadRequest(
        new BadRequestResponse(
          "Não foi possível atualizar a claim",
          _notificador.ObterNotificacoes(),
          userClaim));
    }

    [ClaimsAuthorize("Claims", "Del")]
    [HttpDelete("usuarios-claims/{idClaim:int}")]
    public async Task<ActionResult> ExcluirClaimsParaUsuario(int idClaim)
    {
      if (await _contaServico.ExcluirClaimParaUsuario(idClaim))
        return Ok(new OkResponse("Claim atualizada com sucesso"));

      return BadRequest(
        new BadRequestResponse(
          "Não foi possível atualizar a claim",
          _notificador.ObterNotificacoes()));
    }
  }
}