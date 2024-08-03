using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingZoneWebApi.Models
{
    [Table("ParkingZone")]
    public class ParkingZone
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        public DateTimeOffset CreatedDate { get; set; }

        public ICollection<ParkingSlot>? ParkingSlots { get; set; }
    }
}
