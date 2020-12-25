using System;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Entidades;

namespace Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos
{
    public interface IServicoBase<T> where T : EntidadeBase
    {
         Task Incluir(T entidade);
         Task Editar(T entidade);
         Task Excluir(Guid id);
    }
}