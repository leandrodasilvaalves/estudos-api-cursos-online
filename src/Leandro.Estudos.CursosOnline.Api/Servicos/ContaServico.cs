using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Models;
using Leandro.Estudos.CursosOnline.Api.Notificacoes;
using Microsoft.AspNetCore.Identity;

namespace Leandro.Estudos.CursosOnline.Api.Servicos
{
  public class ContaServico : IContaServico
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly INotificador _notificador;

    public ContaServico(UserManager<AppUser> userManager,
                        SignInManager<AppUser> signInManager,
                        INotificador notificador)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _notificador = notificador;
    }

    public async Task<bool> Logar(ContaLoginModel conta)
    {
      var resultado = await _signInManager.PasswordSignInAsync(conta.Email, conta.Senha,
                                                               isPersistent: true,
                                                               lockoutOnFailure: true);
      return resultado.Succeeded;
    }

    public async Task<bool> Registrar(ContaRegistroModel conta)
    {
      var emailJaCadastrado = await _userManager.FindByEmailAsync(conta.Email);
      if (emailJaCadastrado is not null)
      {
        _notificador.Handle(new Notificacao("Já existe um usuário cadastrado com este e-mail"));
        return false;
      }

      var usuario = new AppUser { UserName = conta.Email, Email = conta.Email };
      var resultado = await _userManager.CreateAsync(usuario, conta.Senha);
      return resultado.Succeeded;
    }
  }
}