using GTL.Domain.Common;

namespace GTL.Application.Contracts;

public interface IRepository<T> where T : AggregateRoot
{
    public Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public T Update(T entity);
    public void Delete(T entity);
}