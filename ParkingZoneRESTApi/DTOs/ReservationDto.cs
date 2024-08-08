using ParkingZoneWebApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneWebApi.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }

        public int Duration { get; set; }

        [Required]
        public DateTime Started { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(5)]
        public string? VehicleNO { get; set; }

        [ForeignKey(nameof(ParkingSlot))]
        public int ParkingSlotId { get; set; }
    }
}
