using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingZoneWebApi.DTOs;
using ParkingZoneWebApi.Models;
using ParkingZoneWebApi.Services.Interfaces;

namespace ParkingZoneWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IParkingSlotService _parkingSlotService;
        private readonly IReservationService _reservationService;
        public ReservationController(IParkingSlotService parkingSlotService, IReservationService reservationService)
        {
            _parkingSlotService = parkingSlotService;
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingSlot>>> GetReservations()
        {
            var reservations = await _reservationService.GetAllAsync();
            if (reservations is null)
                return NotFound();

            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservationById(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);

            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, ReservationDto reservation)
        {
            if (id != reservation.Id)
                return BadRequest();

            try
            {
                await _reservationService.UpdateAsync(reservation.MapToModel());
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError("", $"Database Update Exception: {ex.InnerException?.Message ?? ex.Message}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation(ReservationDto reservation, int slotId)
        {
            var slot = await _parkingSlotService.GetByIdAsync(slotId);
            if (reservation is null || slot is null)
                return BadRequest();

            if (!await _reservationService.CreateAsync(reservation.MapToModel()))
            {
                ModelState.AddModelError("", "Something went wrong while saving the data.");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation is null)
                return NotFound();

            if (!await _reservationService.DeleteAsync(reservation))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the data.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
