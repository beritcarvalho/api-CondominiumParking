namespace CondominiumParkingApi.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(decimal entity);
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<List<T>> UpdateAsync(List<T> entities);
        Task RemoveAsync(T entity);
    }
}
