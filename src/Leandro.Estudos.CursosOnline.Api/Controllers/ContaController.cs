using System.Linq;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
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

    public ContaController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    [AllowAnonymous]
    [HttpPost("registrar")]
    public async Task<ActionResult> Registrar([FromBody] RegistroModel model)
    {
      var emailJaCadastrado = await _userManager.FindByEmailAsync(model.Email);
      if (emailJaCadastrado is not null)
        return BadRequest(new { mensagem = "Já existe um usuário cadastrado com este e-mail" });

      var usuario = new AppUser { UserName = model.Email, Email = model.Email };
      var resultado = await _userManager.CreateAsync(usuario, model.Senha);

      if (resultado.Succeeded)
      {
        await _signInManager.PasswordSignInAsync(model.Email, model.Senha, isPersistent: false, lockoutOnFailure: true);
        return Ok("Usuário registrado com sucesso");
      }

      return BadRequest("Não foi possível registrar o usuário");
    }

    [AllowAnonymous]
    [HttpPost("logar")]
    public async Task<ActionResult> Logar([FromBody] LoginModel model)
    {
      var resultado = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, isPersistent: true, lockoutOnFailure: true);
      if (resultado.Succeeded)
        return Ok("Usuário logado com sucesso");

      return NotFound("Usuário ou senha inválidos");
    }
  }
}