using System.ComponentModel.DataAnnotations;

namespace ParkingZoneWebApi.DTOs
{
    public class ParkingZoneDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "The maximum length allowed is 25 characters.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
    }
}
