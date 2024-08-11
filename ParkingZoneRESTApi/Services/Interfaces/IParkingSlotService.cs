using ParkingZoneWebApi.Models;

namespace ParkingZoneWebApi.Services.Interfaces
{
    public interface IParkingSlotService : IService<ParkingSlot>
    {
        bool HasUniqueSlotNo(IEnumerable<ParkingSlot> slots, int no);

        bool IsFreeForReservation(ParkingSlot slot, Reservation reservation);
    }
}
