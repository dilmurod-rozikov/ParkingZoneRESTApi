using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingZoneWebApi.DTOs;
using ParkingZoneWebApi.Models;
using ParkingZoneWebApi.Services.Interfaces;

namespace ParkingZoneWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSlotsController : ControllerBase
    {
        private readonly IParkingSlotService _parkingSlotService;
        private readonly IParkingZoneService _parkingZoneService;
        public ParkingSlotsController(IParkingSlotService parkingSlotService, IParkingZoneService parkingZoneService)
        {
            _parkingSlotService = parkingSlotService;
            _parkingZoneService = parkingZoneService;
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

            return Ok(parkingSlot);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParkingSlot(int id, ParkingSlotDto parkingSlot)
        {
            if (id != parkingSlot.Id)
                return BadRequest();
            var slot = await _parkingSlotService.GetByIdAsync(id);
            var zone = await _parkingZoneService.GetByIdAsync(parkingSlot.ParkingZoneId);

            if (slot is null || zone is null)
                return NotFound();

            try
            {
                await _parkingSlotService.UpdateAsync(parkingSlot.MapToModel(zone));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError("", $"Database Update Exception: {ex.InnerException?.Message ?? ex.Message}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ParkingSlot>> CreateParkingSlot(ParkingSlotDto parkingSlot)
        {
            var zone = await _parkingZoneService.GetByIdAsync(parkingSlot.ParkingZoneId);
            if(parkingSlot is null || zone is null)
                return BadRequest();

            //Check for unique parking slot number....

            if(!await _parkingSlotService.CreateAsync(parkingSlot.MapToModel(zone)))
            {
                ModelState.AddModelError("", "Something went wrong while saving the data.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
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
