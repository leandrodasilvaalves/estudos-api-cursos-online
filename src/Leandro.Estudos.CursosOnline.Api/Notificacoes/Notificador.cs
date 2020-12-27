using System.Collections.Generic;
using System.Linq;
using Leandro.Estudos.CursosOnline.Api.Interfaces;

namespace Leandro.Estudos.CursosOnline.Api.Notificacoes
{
  public class Notificador : INotificador
  {
    private readonly List<Notificacao> _notificacoes;
    public Notificador()
    {
      _notificacoes = new List<Notificacao>();
    }
    public void Handle(Notificacao notificacao)
    {
      _notificacoes.Add(notificacao);
    }

    public List<Notificacao> ObterNotificacoes()
    {
      return _notificacoes;
    }

    public bool TemNotificacao()
    {
      return _notificacoes.Any();
    }
  }
}