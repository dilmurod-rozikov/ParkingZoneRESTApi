using ParkingZoneWebApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneWebApi.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }

        public uint Duration { get; set; }

        [Required]
        public DateTime Started { get; set; }

        [Required]
        [MaxLength(10)]
        public string VehicleNO { get; set; }

        [Required]
        [ForeignKey(nameof(ParkingSlot))]
        public int ParkingSlotId { get; set; }
    }
}
