using System.Linq;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
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
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJwtServico _jwtServico;

    public ContaController(UserManager<AppUser> userManager,
                           SignInManager<AppUser> signInManager,
                           IJwtServico jwtServico)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _jwtServico = jwtServico;
    }

    [AllowAnonymous]
    [HttpPost("registrar")]
    public async Task<ActionResult> Registrar([FromBody] RegistroModel model)
    {
      if (!ModelState.IsValid)
        return BadRequest(new BadRequestResponse("Os dados informados são inválidos", ModelState, model));

      var emailJaCadastrado = await _userManager.FindByEmailAsync(model.Email);
      if (emailJaCadastrado is not null)
        return BadRequest(new BadRequestResponse("Já existe um usuário cadastrado com este e-mail"));

      var usuario = new AppUser { UserName = model.Email, Email = model.Email };
      var resultado = await _userManager.CreateAsync(usuario, model.Senha);

      if (resultado.Succeeded)
      {
        await _signInManager.PasswordSignInAsync(model.Email, model.Senha, isPersistent: false, lockoutOnFailure: true);
        return Ok(new OkResponse("Usuário registrado com sucesso", token: await _jwtServico.GerarToken(model.Email)));
      }

      return BadRequest(new BadRequestResponse("Não foi possível registrar o usuário"));
    }

    [AllowAnonymous]
    [HttpPost("entrar")]
    public async Task<ActionResult> Logar([FromBody] LoginModel model)
    {
      var resultado = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, isPersistent: true, lockoutOnFailure: true);
      if (resultado.Succeeded)
        return Ok(new OkResponse("Usuário logado com sucesso", token: await _jwtServico.GerarToken(model.Email)));

      return NotFound(new NotFoundResponse("Usuário ou senha inválidos"));
    }
  }
}