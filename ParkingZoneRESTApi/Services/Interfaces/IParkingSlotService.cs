using ParkingZoneWebApi.Models;

namespace ParkingZoneWebApi.Services.Interfaces
{
    public interface IParkingSlotService : IService<ParkingSlot>
    {
        bool HasUniqueSlotNo(IEnumerable<ParkingSlot> slots, int no);

        Task<bool> IsFreeForReservationAsync(ParkingSlot slot, DateTime start, int duration);
    }
}
