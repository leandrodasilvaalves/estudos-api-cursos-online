using System.Linq;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Leandro.Estudos.CursosOnline.Api.Controllers
{

  [ApiController]
  [Authorize]
  [Route("api")]
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
        return Ok(new OkResponse("Usuário registrado com sucesso", token: await _jwtServico.GerarToken(model.Email)));
      }
      return BadRequest(new BadRequestResponse("Não foi possível registrar o usuário", _notificador.ObterNotificacoes(), model));
    }

    [AllowAnonymous]
    [HttpPost("entrar")]
    public async Task<ActionResult> Logar([FromBody] ContaLoginModel model)
    {
      if (await _contaServico.Logar(model))
        return Ok(new OkResponse("Usuário logado com sucesso", token: await _jwtServico.GerarToken(model.Email)));

      return NotFound(new NotFoundResponse("Usuário ou senha inválidos"));
    }
  }
}