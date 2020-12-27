using System.Collections.Generic;
using Leandro.Estudos.CursosOnline.Api.Notificacoes;

namespace Leandro.Estudos.CursosOnline.Api.Interfaces
{
  public interface INotificador
  {
    bool TemNotificacao();
    List<Notificacao> ObterNotificacoes();
    void Handle(Notificacao notificacao);
  }
}