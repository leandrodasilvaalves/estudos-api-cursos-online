using System;
using System.IO;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Models;
using Leandro.Estudos.CursosOnline.Api.Notificacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leandro.Estudos.CursosOnline.Api.Controllers.V2
{
  [ApiController]
  [ApiVersion("2.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  public class AlunosController : ControllerBase
  {
    private readonly IAlunoServico _servico;
    private readonly INotificador _notificador;

    public AlunosController(IAlunoServico servico, INotificador notificador)
    {
      _servico = servico;
      _notificador = notificador;
    }

    [HttpPost]
    public async Task<ActionResult> Post(AlunoComImagemModel aluno)
    {
      await Upload(aluno.ImagemUpload, Guid.NewGuid() + "_");
      return Ok();
    }

    private async Task<bool> Upload(IFormFile arquivo, string prefixo)
    {
      if (arquivo == null || arquivo.Length == 0)
      {
        _notificador.Handle(new Notificacao("Forneça uma imagem para fazer download"));
        return false;
      }

      var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images");
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