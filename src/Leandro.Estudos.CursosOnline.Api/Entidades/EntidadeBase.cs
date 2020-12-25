using System;

namespace Leandro.Estudos.CursosOnline.Api.Entidades
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public bool Ativo { get; set; }
    }
}