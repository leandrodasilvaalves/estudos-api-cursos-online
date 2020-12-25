using System;

namespace Leandro.Estudos.CursosOnline.Api.Entidades
{
    public abstract class EntidadeBase
    {
        public EntidadeBase()
        {
            Id = Guid.NewGuid();
            DataCadastro = DataCadastro == DateTime.MinValue ? DateTime.Now : DataCadastro;
            UltimaAtualizacao = UltimaAtualizacao == DateTime.MinValue ? DateTime.Now : UltimaAtualizacao;
            Ativo = true;
        }
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime UltimaAtualizacao { get; private set; }
        public bool Ativo { get; private set; }
    }
}