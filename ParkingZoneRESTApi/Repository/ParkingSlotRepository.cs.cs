using ParkingZoneWebApi.DataAccess;
using ParkingZoneWebApi.Models;
using ParkingZoneWebApi.Repository.Interfaces;

namespace ParkingZoneWebApi.Repository
{
    public class ParkingSlotRepository : Repository<ParkingSlot>, IParkingSlotRepository
    {
        public ParkingSlotRepository(ApplicationDbContext context) : base(context)
        { }
    }
}
