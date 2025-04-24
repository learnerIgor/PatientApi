using Microsoft.EntityFrameworkCore;
using Patient.Application.Abstractions.Persistence;
using System.Linq.Expressions;

namespace Patient.Persistence;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly PatientDbContext _patientDbContext;

    public BaseRepository(PatientDbContext patientDbContext)
    {
        _patientDbContext = patientDbContext;
    }

    public async Task<TEntity[]> GetListAsync(int? offset = null, int? limit = null, Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, object>>? orderBy = null, bool? descending = null, CancellationToken cancellationToken = default)
    {
        var queryable = _patientDbContext.Set<TEntity>().AsQueryable();

        if (predicate is not null)
        {
            queryable = queryable.Where(predicate);
        }

        if (orderBy is not null)
        {
            queryable = descending == true ? queryable.OrderByDescending(orderBy) : queryable.OrderBy(orderBy);
        }

        if (offset.HasValue)
        {
            queryable = queryable.Skip(offset.Value);
        }

        if (limit.HasValue)
        {
            queryable = queryable.Take(limit.Value);
        }

        return await queryable.ToArrayAsync(cancellationToken);
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        var set = _patientDbContext.Set<TEntity>();
        return predicate == null ? await set.SingleOrDefaultAsync(cancellationToken) : await set.SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var set = _patientDbContext.Set<TEntity>();
        await set.AddAsync(entity, cancellationToken);
        await _patientDbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var set = _patientDbContext.Set<TEntity>();
        set.Update(entity);
        await _patientDbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var set = _patientDbContext.Set<TEntity>();
        set.Remove(entity);
        return await _patientDbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _patientDbContext.Set<TEntity>()
            .Where(predicate)
            .ToArrayAsync(cancellationToken);
    }
}
