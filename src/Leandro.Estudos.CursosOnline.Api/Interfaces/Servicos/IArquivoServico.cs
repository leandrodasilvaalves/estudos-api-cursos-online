using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos
{
  public interface IArquivoServico
  {
    Task<bool> Upload(IFormFile arquivo, string prefixo);
    void Remover(string arquivo);
  }
}