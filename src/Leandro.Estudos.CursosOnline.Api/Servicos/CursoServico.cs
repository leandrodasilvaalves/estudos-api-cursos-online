using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;

namespace Leandro.Estudos.CursosOnline.Api.Servicos
{
  public class CursoServico : ServicoBase<Curso>, ICursoServico
  {
    public CursoServico(ICursoRepositorio repositorio) : base(repositorio) {}
  }
}