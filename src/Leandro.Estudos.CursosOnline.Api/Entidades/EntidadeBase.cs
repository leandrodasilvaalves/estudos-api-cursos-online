using System;

namespace Leandro.Estudos.CursosOnline.Api.Entidades
{
  public abstract class EntidadeBase
  {
    public Guid Id { get; set; }
    public DateTime DataCadastro { get; internal set; }
    public DateTime UltimaAtualizacao { get; internal set; }
    public bool Ativo { get; internal set; }

    public EntidadeBase GerarNovoId()
    {
      Id = Guid.NewGuid();
      return this;
    }
    public EntidadeBase Ativar()
    {
      Ativo = true;
      return this;
    }
    public EntidadeBase Desativar()
    {
      Ativo = false;
      return this;
    }

    public EntidadeBase DefinirDataCadastro()
    {
      DataCadastro = DateTime.Now;
      return this;
    }
    public EntidadeBase DefinirUltimaAtualizacao()
    {
      UltimaAtualizacao = DateTime.Now;
      return this;
    }
  }
}