using System.IO;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Notificacoes;
using Microsoft.AspNetCore.Http;

namespace Leandro.Estudos.CursosOnline.Api.Servicos
{
  public class ArquivoServico : IArquivoServico
  {
    private readonly INotificador _notificador;
    private const string diretorioBase = "wwwroot/assets/images";

    public ArquivoServico(INotificador notificador)
    {
      _notificador = notificador;
    }

    public void Remover(string arquivo)
    {
      var caminhoCompleto = Path.Combine(Directory.GetCurrentDirectory(), diretorioBase, arquivo);
      if (File.Exists(caminhoCompleto))
        File.Delete(caminhoCompleto);
    }

    public async Task<bool> Upload(IFormFile arquivo, string prefixo)
    {
      if (arquivo == null || arquivo.Length == 0)
      {
        _notificador.Handle(new Notificacao("Forneça uma imagem para fazer upload"));
        return false;
      }

      var path = Path.Combine(Directory.GetCurrentDirectory(), diretorioBase);
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);

      var fileName = Path.Combine(path, prefixo + arquivo.FileName);
      if (System.IO.File.Exists(fileName))
      {
        _notificador.Handle(new Notificacao("Já existe um arquivo com este nome!"));
        return false;
      }

      using (var stream = new FileStream(fileName, FileMode.Create))
      {
        await arquivo.CopyToAsync(stream);
      }
      return true;
    }
  }
}