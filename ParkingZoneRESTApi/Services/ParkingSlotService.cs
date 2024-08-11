using ParkingZoneWebApi.Models;
using ParkingZoneWebApi.Repository.Interfaces;
using ParkingZoneWebApi.Services.Interfaces;

namespace ParkingZoneWebApi.Services
{
    public class ParkingSlotService : Service<ParkingSlot>, IParkingSlotService
    {
        public ParkingSlotService(IRepository<ParkingSlot> repository)
            : base(repository) { }

        public bool HasUniqueSlotNo(IEnumerable<ParkingSlot> slots, int no)
        {
            return slots.Any(x => x.No == no);
        }
    }
}
