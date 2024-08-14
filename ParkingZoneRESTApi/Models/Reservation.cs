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
        public int Duration { get; set; } = 1;

        public DateTime Started { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(10)]
        [MinLength(5)]
        public string? VehicleNO { get; set; }

        [ForeignKey(nameof(ParkingSlot))]
        public int ParkingSlotId { get; set; }

        public virtual ParkingSlot? ParkingSlot { get; set; }
    }
}
