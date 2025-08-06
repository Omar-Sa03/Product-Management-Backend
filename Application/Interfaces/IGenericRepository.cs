namespace Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Post(T entity);
        Task<List<T>> PostRange(List<T> entity);
        Task<List<T>> GetAll();
        Task<T?> GetById(Guid id);
        Task Update(T entity);
        Task Delete(Guid id);
        Task<bool> Exists(Guid id);
        Task SaveChange(CancellationToken cancellationToken);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> SoftDelete(Guid id);
    }
}
