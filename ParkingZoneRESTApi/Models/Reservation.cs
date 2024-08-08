using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingZoneWebApi.Models
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Range(0, uint.MaxValue)]
        public int Duration { get; set; }

        public DateTime Started { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(5)]
        public string? VehicleNO { get; set; }

        [ForeignKey(nameof(ParkingSlot))]
        public int ParkingSlotId { get; set; }

        public ParkingSlot? ParkingSlot { get; set; }
    }
}
