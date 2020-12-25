using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;

namespace Leandro.Estudos.CursosOnline.Api.Servicos
{
  public class AlunoServico : ServicoBase<Aluno>, IAlunoServico
  {
    public AlunoServico(IAlunoRepositorio repositorio) : base(repositorio){}
  }
}