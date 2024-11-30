using GTL.Domain.Common;

namespace GTL.Application.Contracts;

public interface IRepository<T> where T : AggregateRoot
{
    public Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    public Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    public Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = default);
}