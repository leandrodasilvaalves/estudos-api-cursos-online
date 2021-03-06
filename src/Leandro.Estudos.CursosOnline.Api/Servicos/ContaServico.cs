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

    public async Task<AppUser> ObterPorUsuarioId(Guid id)
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

    public async Task<bool> CadastrarClaimParaUsuario(IdentityUserClaim<Guid> userClaim)
    {
      if (!ExecutarValidacao(new UserClaimValidations(), userClaim)) return false;
      if (await UsuarioPossuiClaim(userClaim.UserId, userClaim.ClaimType))
      {
        var mensagemErro = "O usuário já possui este claim. Atualize o registro invés de cadastrar um novo";
        _notificador.Handle(new Notificacao(mensagemErro));
        return false;
      }

      await _contexto.UserClaims.AddAsync(userClaim);
      await _contexto.SaveChangesAsync();
      return true;
    }

    public async Task<bool> UsuarioPossuiClaim(Guid userId, string claimType)
    {
      return (await _contexto.UserClaims
                      .AsNoTracking()
                      .FirstOrDefaultAsync(c =>
                          c.UserId == userId &&
                          c.ClaimType == claimType)
              ) != null;
    }

    public async Task<IdentityUserClaim<Guid>> ObterClaimPorId(int id)
    {
      return await _contexto.UserClaims
                        .AsNoTracking()
                        .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> AtualizarClaimParaUsuario(IdentityUserClaim<Guid> userClaim)
    {
      if (!ExecutarValidacao(new UserClaimValidations(), userClaim)) return false;
      _contexto.Entry(userClaim).State = EntityState.Modified;
      var salvoComSucesso = 1;
      return (await _contexto.SaveChangesAsync() == salvoComSucesso);
    }

    public async Task<bool> ExcluirClaimParaUsuario(int id)
    {
      var usuarioClaim = await _contexto.UserClaims
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync(c => c.Id == id);

      if (usuarioClaim == null)
      {
        _notificador.Handle(new Notificacao("A claim não foi localizada na base de dados"));
        return false;
      }
      _contexto.Remove(usuarioClaim);
      var excluidoComSucesso = 1;
      return (await _contexto.SaveChangesAsync() == excluidoComSucesso);
    }
  }
}