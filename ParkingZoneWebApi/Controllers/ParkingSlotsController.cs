using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingZoneWebApi.DataAccess;
using ParkingZoneWebApi.Models;
using ParkingZoneWebApi.Services.Interfaces;

namespace ParkingZoneWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSlotsController : ControllerBase
    {
        private readonly IParkingSlotService _parkingSlotService;

        public ParkingSlotsController(IParkingSlotService parkingSlotService)
        {
            _parkingSlotService = parkingSlotService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingSlot>>> GetParkingSlots()
        {
            var slots = await _parkingSlotService.GetAllAsync();
            if (slots is null)
                return NotFound();

            return Ok(slots);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSlot>> GetParkingSlotById(int id)
        {
            var parkingSlot = await _parkingSlotService.GetByIdAsync(id);

            if (parkingSlot == null)
                return NotFound();

            return parkingSlot;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParkingSlot(int id, ParkingSlot parkingSlot)
        {
            if (id != parkingSlot.Id)
                return BadRequest();
            
            try
            {
                await _parkingSlotService.UpdateAsync(parkingSlot);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError("", $"Database Update Exception: {ex.InnerException?.Message ?? ex.Message}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ParkingSlot>> PostParkingSlot(ParkingSlot parkingSlot)
        {
            if(!await _parkingSlotService.CreateAsync(parkingSlot))
            {
                ModelState.AddModelError("", "Something went wrong while saving the data.");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetParkingSlot", new { id = parkingSlot.Id }, parkingSlot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingSlot(int id)
        {
            var slot = await _parkingSlotService.GetByIdAsync(id);
            if (slot is null)
                return NotFound();

            if (!await _parkingSlotService.DeleteAsync(slot))
            {
                ModelState.AddModelError("", "Something went wrong while saving the data.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
