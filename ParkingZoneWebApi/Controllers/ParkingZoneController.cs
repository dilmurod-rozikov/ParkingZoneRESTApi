using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingZoneWebApi.DTOs;
using ParkingZoneWebApi.Services.Interfaces;

namespace ParkingZoneWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingZoneController : ControllerBase
    {
        private readonly IParkingZoneService _parkingZoneService;

        public ParkingZoneController(IParkingZoneService parkingZoneService)
        {
            _parkingZoneService = parkingZoneService;
        }

        [HttpGet]
        public async Task<IActionResult> GetParkingZones()
        {
            var zones = await _parkingZoneService.GetAllAsync();
            if (zones is null)
                return NotFound();

            return Ok(zones);
        }

        [HttpGet("ParkingZone/{id}")]
        public async Task<IActionResult> GetParkingZoneById(int id)
        {
            var zone = await _parkingZoneService.GetByIdAsync(id);
            if (zone is null)
                return NotFound();

            return Ok(zone);
        }

        [HttpPost]
        public async Task<IActionResult> CreateParkingZone(ParkingZoneDto zoneDto)
        {
            if (zoneDto is null)
                return NotFound();

            //return already exsist if title + address is the same

            try
            {
                if (!await _parkingZoneService.CreateAsync(zoneDto.MapToModel()))
                {
                    ModelState.AddModelError("", "Something went wrong while saving the data.");
                    return StatusCode(500, ModelState);
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", $"Database Update Exception: {ex.InnerException?.Message ?? ex.Message}");
                return StatusCode(500, ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateParkingZone(int id, ParkingZoneDto zoneDto)
        {
            var getZone = await _parkingZoneService.GetByIdAsync(id);

            if (zoneDto is null || getZone is null)
                return NotFound();

            if(!await _parkingZoneService.UpdateAsync(zoneDto.MapToModel()))
            {
                ModelState.AddModelError("", "Something went wrong in the server while updating the data.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingZone(int id)
        {
            var zone = await _parkingZoneService.GetByIdAsync(id);
            if (zone is null)
                return NotFound();
            if(!await _parkingZoneService.DeleteAsync(zone))
            {
                ModelState.AddModelError("", "Something went wrong in the server while deleting the data.");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

    }
}
