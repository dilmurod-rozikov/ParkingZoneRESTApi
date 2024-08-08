using ParkingZoneWebApi.Repository.Interfaces;
using ParkingZoneWebApi.Services.Interfaces;

namespace ParkingZoneWebApi.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;
        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<bool> CreateAsync(T entity)
        {
            return await _repository.Insert(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.Update(entity);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            return await _repository.Delete(entity);
        }
    }
}
