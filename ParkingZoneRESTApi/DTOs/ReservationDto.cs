using ParkingZoneWebApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneWebApi.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int Duration { get; set; } = 1;

        [Required]
        public DateTime Started { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(10)]
        [MinLength(5)]
        public string? VehicleNO { get; set; }

        [ForeignKey(nameof(ParkingSlot))]
        public int ParkingSlotId { get; set; }
    }
}
