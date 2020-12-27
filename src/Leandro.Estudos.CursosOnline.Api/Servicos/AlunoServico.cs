using System;
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
  public class AlunoServico : ServicoBase<Aluno>, IAlunoServico
  {
    private readonly IAlunoRepositorio _repositorio;
    private readonly INotificador _notificador;
    public AlunoServico(IAlunoRepositorio repositorio, INotificador notificador)
      : base(repositorio, notificador)
    {
      _repositorio = repositorio;
      _notificador = notificador;
    }

    public override async Task<bool> Incluir(Aluno entidade)
    {
      if (!ExecutarValidacao(new AlunoValidacoes(), entidade)) return false;

      var emailCadastrado = await _repositorio.Buscar(a => a.Email.Equals(entidade.Email));
      if (emailCadastrado.Any())
      {
        _notificador.Handle(new Notificacao("Já existe um aluno cadastrado com este e-mail"));
        return false;
      }

      await _repositorio.Incluir(entidade);
      return true;
    }

    public override async Task<bool> Editar(Aluno entidade)
    {
      if (!ExecutarValidacao(new AlunoValidacoes(), entidade)) return false;
      await _repositorio.Editar(entidade);
      return true;
    }
  }
}