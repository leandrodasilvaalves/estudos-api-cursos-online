using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos
{
  public interface IContaServico
  {
    Task<bool> Registrar(ContaRegistroModel conta);
    Task<bool> Logar(ContaLoginModel conta);
    Task<bool> TrocarSenha(AppUser usuario, ContaTrocaSenhaModel conta);
    Task<AppUser> ObterPorUsuarioId(Guid id);
    Task<IEnumerable<ContaClaimsModel>> ObterUsuariosComClaims();
    Task<bool> CadastrarClaimParaUsuario(IdentityUserClaim<Guid> userClaim);
    Task<bool> AtualizarClaimParaUsuario(IdentityUserClaim<Guid> userClaim);
    Task<bool> UsuarioPossuiClaim(Guid userId, string claimType);
    Task<IdentityUserClaim<Guid>> ObterClaimPorId(int id);
    Task<bool> ExcluirClaimParaUsuario(int id);
  }
}