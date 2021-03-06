using System;
using Leandro.Estudos.CursosOnline.Api.Extensoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leandro.Estudos.CursosOnline.Api.Models
{
  [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "aluno")]
  public class AlunoComImagemModel
  {
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public IFormFile ImagemUpload { get; set; }
    internal string Imagem { get; set; }
  }

  public class AlunoSemImagemModel
  {
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
  }
}