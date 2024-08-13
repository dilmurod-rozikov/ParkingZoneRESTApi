using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingZoneWebApi.Models
{
    [Table("ParkingZone")]
    public class ParkingZone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "The maximum length allowed is 25 characters.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The maximum length allowed is 100 characters.")]
        public string Address { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public virtual ICollection<ParkingSlot>? ParkingSlots { get; set; }
    }
}
