namespace Leandro.Estudos.CursosOnline.Api.Models
{
  public abstract class ResponseModel
  {
    protected ResponseModel(string mensagem, object dados, bool sucesso, int statusCode)
    {
      Sucesso = sucesso;
      StatusCode = statusCode;
      Mensagem = mensagem;
      Dados = dados;
    }

    public bool Sucesso { get; private set; }
    public int StatusCode { get; private set; }
    public string Mensagem { get; private set; }
    public object Dados { get; private set; }
  }

  public class OkResponse : ResponseModel
  {
    public OkResponse()
        : base(mensagem: "", dados: null, sucesso: true, statusCode: 200) { }
    public OkResponse(object dados)
        : base(mensagem: "", dados, sucesso: true, statusCode: 200) { }
    public OkResponse(string mensagem, object dados = null, string token = null)
        : base(mensagem, dados, sucesso: true, statusCode: 200)
    { this.Token = token; }

    public string Token { get; private set; }
  }

  public class BadRequestResponse : ResponseModel
  {
    public BadRequestResponse(string mensagem)
      : base(mensagem, dados: null, sucesso: false, statusCode: 400) { }
  }

  public class NotFoundResponse : ResponseModel
  {
    public NotFoundResponse(string mensagem)
      : base(mensagem, dados: null, sucesso: false, statusCode: 404) { }
  }
}