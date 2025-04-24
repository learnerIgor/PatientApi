using System.Linq.Expressions;

namespace Patient.Application.Abstractions.Persistence
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity[]> GetListAsync(
            int? offset = null,
            int? limit = null,
            Expression<Func<TEntity, bool>>? expression = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            bool? descending = null,
            CancellationToken cancellationToken = default);

        Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
