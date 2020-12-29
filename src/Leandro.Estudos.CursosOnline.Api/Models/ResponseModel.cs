using System.Collections.Generic;
using System.Linq;
using Leandro.Estudos.CursosOnline.Api.Notificacoes;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Leandro.Estudos.CursosOnline.Api.Models
{
  public abstract class ResponseModel
  {
    protected ResponseModel(string mensagem, object dados, bool sucesso)
    {
      Sucesso = sucesso;
      Mensagem = mensagem;
      Dados = dados;
    }

    public bool Sucesso { get; private set; }
    public abstract int StatusCode { get; }
    public string Mensagem { get; private set; }
    public object Dados { get; private set; }
  }

  public class OkResponse : ResponseModel
  {
    public OkResponse()
        : base(mensagem: "", dados: null, sucesso: true) { }
    public OkResponse(object dados)
        : base(mensagem: "", dados, sucesso: true) { }
    public OkResponse(string mensagem, object dados)
        : base(mensagem, dados, sucesso: true) { }

    public override int StatusCode => 200;
  }

  public class OkAuthResponse : OkResponse
  {
    public OkAuthResponse(string mensagem, object dados = null, string token = null)
        : base(mensagem, dados)
    { this.Token = token; }

    public string Token { get; private set; }
  }

  public class BadRequestResponse : ResponseModel
  {
    public BadRequestResponse(string mensagem)
      : base(mensagem, dados: null, sucesso: false) { }
    public BadRequestResponse(string mensagem, List<Notificacao> erros, object dados)
      : base(mensagem, dados, sucesso: false)
        => Erros = (from erro in erros select erro.Mensagem).ToList();

    public BadRequestResponse(string mensagem, ModelStateDictionary modelState, object dados)
      : base(mensagem, dados, sucesso: false)
    {
      var erros = modelState.Values.SelectMany(e => e.Errors);
      Erros = new List<string>();
      foreach (var erro in erros)
      {
        var erroMensagem = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
        Erros.Add(erroMensagem);
      }
    }
    public List<string> Erros { get; private set; }
    public override int StatusCode => 400;
  }

  public class NotFoundResponse : ResponseModel
  {
    public NotFoundResponse(string mensagem)
      : base(mensagem, dados: null, sucesso: false) { }
    public override int StatusCode => 404;
  }
}