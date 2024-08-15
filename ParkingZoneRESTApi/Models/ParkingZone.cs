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
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The maximum length allowed is 100 characters.")]
        [MinLength(3)]
        public string Address { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public virtual ICollection<ParkingSlot>? ParkingSlots { get; set; }
    }
}
