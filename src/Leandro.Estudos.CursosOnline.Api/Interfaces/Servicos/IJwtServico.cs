using System.Threading.Tasks;

namespace Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos
{
  public interface IJwtServico
  {
    Task<string> GerarToken(string email);
  }
}