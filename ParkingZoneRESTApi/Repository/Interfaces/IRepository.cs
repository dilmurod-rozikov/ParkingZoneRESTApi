namespace ParkingZoneWebApi.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<bool> Delete(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(int id);
        Task<bool> Insert(T entity);
        Task Update(T entity);
    }
}