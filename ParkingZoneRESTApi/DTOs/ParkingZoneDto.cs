using System.ComponentModel.DataAnnotations;

namespace ParkingZoneWebApi.DTOs
{
    public class ParkingZoneDto
    {
        public int Id { get; set; }

        [MaxLength(25, ErrorMessage = "The maximum length allowed is 25 characters.")]
        [MinLength(3)]
        public string Title { get; set; }

        [MaxLength(100)]
        [MinLength(3)]
        public string Address { get; set; }
    }
}
