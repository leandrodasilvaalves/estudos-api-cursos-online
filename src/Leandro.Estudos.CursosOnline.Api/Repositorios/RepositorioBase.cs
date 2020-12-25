using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Leandro.Estudos.CursosOnline.Api.Contexts;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Leandro.Estudos.CursosOnline.Api.Repositorios
{
  public abstract class RepositorioBase<T> : IRepositorioBase<T>, IDisposable where T : EntidadeBase, new()
  {
    protected readonly CursoContext _db;
    protected readonly DbSet<T> _dbSet;

    protected RepositorioBase(CursoContext db)
    {
      _db = db;
      _dbSet = db.Set<T>();
    }

    public async Task<IEnumerable<T>> Listar()
    {
      return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T> ObterPorId(Guid id)
    {
      return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate)
    {
      return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task Incluir(T entidade)
    {
      _dbSet.Add(entidade);
      await Salvar();
    }

    public async Task Editar(T entidade)
    {
      _dbSet.Update(entidade);
      await Salvar();
    }

    public async Task Excluir(Guid id)
    {
      _dbSet.Remove(new T { Id = id });
      await Salvar();
    }
    public async Task<int> Salvar()
    {
      return await _db.SaveChangesAsync();
    }

    public void Dispose()
    {
      _db?.Dispose();
    }
  }
}