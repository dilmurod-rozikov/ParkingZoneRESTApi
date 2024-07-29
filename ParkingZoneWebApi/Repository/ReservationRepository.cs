using ParkingZoneWebApi.DataAccess;
using ParkingZoneWebApi.Models;
using ParkingZoneWebApi.Repository.Interfaces;

namespace ParkingZoneWebApi.Repository
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext context) : base(context)
        { }
    }
}
