using ParkingZoneWebApi.Enums;
using ParkingZoneWebApi.Models;

namespace ParkingZoneWebApi.Services.Interfaces
{
    public interface IParkingSlotService : IService<ParkingSlot>
    {
        bool HasUniqueSlotNo(IEnumerable<ParkingSlot> slots, int no);

        bool IsFreeForReservation(ParkingSlot slot, DateTime start, int duration);

        public IEnumerable<ParkingSlot> GetSlotsByCategory(IEnumerable<ParkingSlot> slots, Category category);
    }
}
