using ParkingZoneWebApi.Models;

namespace ParkingZoneWebApi.Services.Interfaces
{
	public interface IParkingZoneService : IService<ParkingZone>
	{
		bool HasUniqueTitleAndAddress(IEnumerable<ParkingZone> parkingZones, string title, string address);

		public new Task CreateAsync(ParkingZone zone);

		public IEnumerable<ParkingZone> SearchByTitle(string title);

		public IEnumerable<ParkingZone> SearchByAddress(string address);
	}
}
