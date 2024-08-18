using ParkingZoneWebApi.Models;
using ParkingZoneWebApi.Repository.Interfaces;
using ParkingZoneWebApi.Services.Interfaces;

namespace ParkingZoneWebApi.Services
{
    public class ReservationService : Service<Reservation>, IReservationService
    {
        public ReservationService(IRepository<Reservation> repository)
            : base(repository) { }

        public IEnumerable<Reservation> GetReservationsBySlotId(ParkingSlot slot)
        {
            return slot.Reservations!;
        }
    }
}
