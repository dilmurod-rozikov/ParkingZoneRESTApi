using ParkingZoneWebApi.DataAccess;
using ParkingZoneWebApi.Models;
using ParkingZoneWebApi.Repository.Interfaces;

namespace ParkingZoneWebApi.Repository
{
    public class ParkingZoneRepository : Repository<ParkingZone>, IParkingZoneRepository
    {
        public ParkingZoneRepository(ApplicationDbContext context) : base(context)
        { }
    }
}
