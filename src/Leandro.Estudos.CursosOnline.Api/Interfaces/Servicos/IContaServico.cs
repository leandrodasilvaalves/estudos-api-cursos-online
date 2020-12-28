using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Models;

namespace Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos
{
  public interface IContaServico
  {
    Task<bool> Registrar(ContaRegistroModel conta);
    Task<bool> Logar(ContaLoginModel conta);
  }
}