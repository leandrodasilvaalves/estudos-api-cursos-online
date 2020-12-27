using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Validacoes;
using System.Linq;
using Leandro.Estudos.CursosOnline.Api.Notificacoes;

namespace Leandro.Estudos.CursosOnline.Api.Servicos
{
  public class CursoServico : ServicoBase<Curso>, ICursoServico
  {
    private readonly ICursoRepositorio _repositorio;
    private readonly INotificador _notificador;

    public CursoServico(ICursoRepositorio repositorio, INotificador notificador)
      : base(repositorio, notificador)
    {
      _repositorio = repositorio;
      _notificador = notificador;
    }

    public override async Task<bool> Incluir(Curso entidade)
    {
      if (!ExecutarValidacao(new CursoValidacoes(), entidade)) return false;
      var nomeCadastrado = await _repositorio.Buscar(c => c.Nome.Equals(entidade.Nome));
      if (nomeCadastrado.Any())
      {
        _notificador.Handle(new Notificacao("Já existe um curso cadastrado com este nome"));
        return false;
      }

      await _repositorio.Incluir(entidade);
      return true;
    }

    public override async Task<bool> Editar(Curso entidade)
    {
      if (!ExecutarValidacao(new CursoValidacoes(), entidade)) return false;
      await _repositorio.Editar(entidade);
      return true;
    }
  }
}