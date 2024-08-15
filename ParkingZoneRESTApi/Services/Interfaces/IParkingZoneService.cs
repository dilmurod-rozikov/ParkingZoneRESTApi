using ParkingZoneWebApi.Models;

namespace ParkingZoneWebApi.Services.Interfaces
{
	public interface IParkingZoneService : IService<ParkingZone>
	{
		bool HasUniqueTitleAndAddress(IEnumerable<ParkingZone> parkingZones, string title, string address);

		public new Task CreateAsync(ParkingZone zone);

		public Task SearchByTitleOrAddress(string text);
	}
}
