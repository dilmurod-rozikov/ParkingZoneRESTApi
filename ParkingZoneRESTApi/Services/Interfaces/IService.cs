namespace ParkingZoneWebApi.Services.Interfaces
{
    public interface IService<T>
    {
        Task<bool> DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<bool> CreateAsync(T entity);
        Task UpdateAsync(T entity);
    }
}