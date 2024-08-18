using ParkingZoneWebApi.Models;

namespace ParkingZoneWebApi.Services.Interfaces
{
    public interface IReservationService : IService<Reservation> 
    {

        public IEnumerable<Reservation> GetReservationsBySlotId(ParkingSlot slot);
    }
}
