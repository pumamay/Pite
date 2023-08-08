using Microsoft.EntityFrameworkCore;
using Proyecto.PiteApi.Data;
using Proyecto.PiteApi.Interfaces.Contracts;
using System.Linq.Expressions;

namespace Proyecto.PiteApi.Helpers;

public class BaseRepository<TEntity> : IRepository<TEntity>, IReadRepository<TEntity> where TEntity : class
{
    protected readonly PiteDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(PiteDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public IUnitOfWork UnitOfWork
    {
        get
        {
            return _context;
        }
    }

    public virtual IQueryable<TEntity> GetAll(bool asNoTracking = true)
    {
        if (asNoTracking)
            return _dbSet.AsNoTracking();
        else
            return _dbSet.AsQueryable();
    }

    public virtual IQueryable<TEntity> GetAllBySpec(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true)
    {
        if (asNoTracking)
            return _dbSet.Where(predicate).AsNoTracking();
        else
            return _dbSet.Where(predicate).AsQueryable();
    }

    public virtual async Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
    }

    public virtual async Task<TEntity?> GetBySpecAsync<Spec>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual async Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual async Task<ICollection<TEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(predicate).CountAsync(cancellationToken);
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(cancellationToken);
    }

    public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> queryable = GetAll();
        foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
        {
            queryable = queryable.Include(includeProperty);
        }

        return queryable;
    }
    public virtual TEntity Add(TEntity entity)
    {
        return _dbSet.Add(entity).Entity;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual ICollection<TEntity> AddRange(ICollection<TEntity> entities)
    {
        _dbSet.AddRange(entities);

        return entities;
    }

    public virtual async Task<int> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities);

        return await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);

        return await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual void DeleteRange(ICollection<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public virtual async Task<int> DeleteRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
    {
        _dbSet.RemoveRange(entities);

        return await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public virtual async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
