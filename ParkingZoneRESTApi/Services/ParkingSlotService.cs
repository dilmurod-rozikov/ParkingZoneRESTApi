using ParkingZoneWebApi.Enums;
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

        public bool IsFreeForReservation(ParkingSlot slot, DateTime started, int duration)
        {
            return slot.Reservations!.Any(x => ((slot.IsAvailable) &
                (started >= x.Started && started.AddHours(duration) <= x.Started.AddHours(x.Duration))) ||
                (started >= x.Started && started < x.Started.AddHours(x.Duration)) ||
                (started <= x.Started && x.Started < started.AddHours(duration)));
        }

        public IEnumerable<ParkingSlot> GetSlotsByCategory(Category category)
        {
            return GetAllAsync().Result.Where(x => x.Category == category);
        }
    }
}
