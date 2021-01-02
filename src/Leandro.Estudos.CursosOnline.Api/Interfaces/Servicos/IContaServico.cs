using System;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Models;

namespace Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos
{
  public interface IContaServico
  {
    Task<bool> Registrar(ContaRegistroModel conta);
    Task<bool> Logar(ContaLoginModel conta);
    Task<bool> TrocarSenha(AppUser usuario, ContaTrocaSenhaModel conta);
    Task<AppUser> ObterPorId(Guid id);
  }
}