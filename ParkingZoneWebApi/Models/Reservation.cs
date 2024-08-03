using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingZoneWebApi.Models
{
    [Table("Reservation")]
    public class Reservation
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public uint Duration { get; set; }

        [Required]
        public DateTime Started { get; set; }

        [Required]
        [MaxLength(10)]
        public string VehicleNO { get; set; }

        [Required]
        [ForeignKey(nameof(ParkingSlot))]
        public int ParkingSlotId { get; set; }

        [Required]
        public ParkingSlot? ParkingSlot { get; set; }
    }
}
