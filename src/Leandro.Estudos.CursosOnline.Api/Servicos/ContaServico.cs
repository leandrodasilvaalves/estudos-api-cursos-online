using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Contexts;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Models;
using Leandro.Estudos.CursosOnline.Api.Notificacoes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Leandro.Estudos.CursosOnline.Api.Validacoes;

namespace Leandro.Estudos.CursosOnline.Api.Servicos
{
  public class ContaServico : AbstractValidacaoServico<IdentityUserClaim<Guid>>, IContaServico
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly INotificador _notificador;

    private readonly IdentityAppContext _contexto;

    public ContaServico(UserManager<AppUser> userManager,
                        SignInManager<AppUser> signInManager,
                        INotificador notificador,
                        IdentityAppContext contexto)
                        : base(notificador)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _notificador = notificador;
      _contexto = contexto;
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

    public async Task<bool> TrocarSenha(AppUser usuario, ContaTrocaSenhaModel conta)
    {
      var result = await _userManager.ChangePasswordAsync(usuario, conta.SenhaAtual, conta.NovaSenha);
      if (result.Succeeded)
        return true;

      foreach (var erro in result.Errors)
        _notificador.Handle(new Notificacao(erro.Description));
      return false;
    }

    public async Task<AppUser> ObterPorId(Guid id)
    {
      return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<IEnumerable<ContaClaimsModel>> ObterUsuariosComClaims()
    {
      return await (from user in _contexto.Users.AsNoTracking()
                    select
                      new ContaClaimsModel(
                              user,
                              _contexto.UserClaims
                                .AsNoTracking()
                                .Where(c => c.UserId == user.Id)
                                .ToList())
                    ).ToListAsync();
    }

    public async Task<bool> CadastrarClaim(IdentityUserClaim<Guid> userClaim)
    {
      if (!ExecutarValidacao(new UserClaimValidations(), userClaim)) return false;
      await _contexto.UserClaims.AddAsync(userClaim);
      await _contexto.SaveChangesAsync();
      return true;
    }
  }
}