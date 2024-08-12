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

        public async Task<bool> IsFreeForReservationAsync(ParkingSlot slot, DateTime started, int duration)
        {
            return slot.Reservations!.Any(x =>
                (started >= x.Started && started.AddHours(duration) <= x.Started.AddHours(x.Duration)) ||
                (started >= x.Started && started < x.Started.AddHours(x.Duration)) ||
                (started <= x.Started && x.Started < started.AddHours(duration)));
        }
    }
}
