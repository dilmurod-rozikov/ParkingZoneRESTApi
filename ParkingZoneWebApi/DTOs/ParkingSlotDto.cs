using ParkingZoneWebApi.Enums;
using ParkingZoneWebApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneWebApi.DTOs
{
    public class ParkingSlotDto
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public uint No { get; set; }

        [Required]
        public int ParkingZoneId { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        [EnumDataType(typeof(Category))]
        public Category Category { get; set; } = Category.Standard;

        public ParkingSlotDto(int id, uint no, bool isAvailable, int parkingZoneId, Category category)
        {
            Id = id;
            No = no;
            IsAvailable = isAvailable;
            ParkingZoneId = parkingZoneId;
            Category = category;
        }

        public ParkingSlot MapToModel()
        {
            return new()
            {
                Id = Id,
                No = No,
                IsAvailable = IsAvailable,
                ParkingZoneId = ParkingZoneId,
                Category = Category
            };
        }
    }
}
