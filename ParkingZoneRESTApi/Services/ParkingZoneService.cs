using ParkingZoneWebApi.Models;
using ParkingZoneWebApi.Repository.Interfaces;
using ParkingZoneWebApi.Services.Interfaces;

namespace ParkingZoneWebApi.Services
{
    public class ParkingZoneService : Service<ParkingZone>, IParkingZoneService
    {
        public ParkingZoneService(IRepository<ParkingZone> repository)
            : base(repository) { }

        public bool HasUniqueTitleAndAddress(IEnumerable<ParkingZone> parkingZones, string title, string address)
        {
            return parkingZones.Any(x => x.Address == address && x.Title == title);
        }

        public async new Task CreateAsync(ParkingZone zone)
        {
            zone.CreatedDate = DateTime.Now;
            await base.CreateAsync(zone);
        }

        public async Task<IEnumerable<ParkingZone>> SearchByTitle(string title)
        {
            return GetAllAsync().Result.Where(x => x.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<ParkingZone>> SearchByAddress(string address)
        {
            return GetAllAsync().Result.Where(x => x.Address.Equals(address, StringComparison.OrdinalIgnoreCase));
        }
    }
}
