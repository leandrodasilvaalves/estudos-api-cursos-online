using System.Collections.Generic;
using System.Linq;
using Leandro.Estudos.CursosOnline.Api.Notificacoes;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Leandro.Estudos.CursosOnline.Api.Models
{
  public abstract class ResponseModel
  {
    protected ResponseModel(string mensagem, object dados)
    {
      Mensagem = mensagem;
      Dados = dados;
    }

    public abstract bool Sucesso { get; }
    public abstract int StatusCode { get; }
    public string Mensagem { get; private set; }
    public object Dados { get; private set; }
  }

  public class OkResponse : ResponseModel
  {
    public OkResponse()
        : base(mensagem: "", dados: null) { }
    public OkResponse(object dados)
        : base(mensagem: "", dados) { }
    public OkResponse(string mensagem, object dados)
        : base(mensagem, dados) { }

    public override int StatusCode => 200;

    public override bool Sucesso => true;
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
      : base(mensagem, dados: null) { }
    public BadRequestResponse(string mensagem, List<Notificacao> erros)
      : base(mensagem, dados: null)
        => Erros = (from erro in erros select erro.Mensagem).ToList();
    public BadRequestResponse(string mensagem, List<Notificacao> erros, object dados)
      : base(mensagem, dados)
        => Erros = (from erro in erros select erro.Mensagem).ToList();

    public BadRequestResponse(string mensagem, ModelStateDictionary modelState, object dados)
      : base(mensagem, dados)
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

    public override bool Sucesso => false;
  }

  public class NotFoundResponse : ResponseModel
  {
    public NotFoundResponse(string mensagem)
      : base(mensagem, dados: null) { }

    public NotFoundResponse(string mensagem, object dados)
      : base(mensagem, dados) { }
    public override int StatusCode => 404;

    public override bool Sucesso => false;
  }
}