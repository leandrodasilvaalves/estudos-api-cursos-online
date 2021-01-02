using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Models;
using Microsoft.AspNetCore.Authorization;
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
  }
}