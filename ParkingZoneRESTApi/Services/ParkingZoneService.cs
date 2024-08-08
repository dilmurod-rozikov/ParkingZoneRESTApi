using ParkingZoneWebApi.Models;
using ParkingZoneWebApi.Repository.Interfaces;
using ParkingZoneWebApi.Services.Interfaces;

namespace ParkingZoneWebApi.Services
{
    public class ParkingZoneService : Service<ParkingZone>, IParkingZoneService
    {
        public ParkingZoneService(IRepository<ParkingZone> repository)
            : base(repository) { }
    }
}
