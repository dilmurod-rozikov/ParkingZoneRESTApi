using ParkingZoneWebApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneWebApi.DTOs
{
    public class ParkingSlotDto
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int No { get; set; }

        public int ParkingZoneId { get; set; }

        public bool IsAvailable { get; set; } = true;

        [EnumDataType(typeof(Category))]
        public Category Category { get; set; } = Category.Standard;
    }
}
