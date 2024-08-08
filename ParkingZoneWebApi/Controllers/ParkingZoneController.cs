using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingZoneWebApi.DTOs;
using ParkingZoneWebApi.Models;
using ParkingZoneWebApi.Services.Interfaces;

namespace ParkingZoneWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingZoneController : ControllerBase
    {
        private readonly IParkingZoneService _parkingZoneService;
        private readonly IMapper _mapper;

        public ParkingZoneController(IParkingZoneService parkingZoneService, IMapper mapper)
        {
            _parkingZoneService = parkingZoneService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingZoneDto>>> GetParkingZones()
        {
            var zones = _mapper.Map<List<ParkingZoneDto>>(await _parkingZoneService.GetAllAsync());
            if (zones is null)
                return NotFound();

            return Ok(zones);
        }

        [HttpGet("ParkingZone/{id}")]
        public async Task<ActionResult<ParkingZone>> GetParkingZoneById(int id)
        {
            var zone = _mapper.Map<ParkingZoneDto>(await _parkingZoneService.GetByIdAsync(id));
            if (zone is null)
                return NotFound();

            return Ok(zone);
        }

        [HttpPost]
        public async Task<ActionResult<ParkingZone>> CreateParkingZone(ParkingZoneDto zoneDto)
        {
            if (zoneDto is null)
                return NotFound();

            //return already exsist if title + address is the same

            try
            {
                await _parkingZoneService.CreateAsync(_mapper.Map<ParkingZone>(zoneDto));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", $"Database Update Exception: {ex.InnerException?.Message ?? ex.Message}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateParkingZone(int id, ParkingZoneDto zoneDto)
        {
            var exist = await ParkingZoneExists(id);
            if (!exist)
                return NotFound();

            if(id != zoneDto.Id || zoneDto is null)
                return BadRequest();

            try
            {
                await _parkingZoneService.UpdateAsync(_mapper.Map<ParkingZone>(zoneDto));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError("", $"Database Update Exception: {ex.InnerException?.Message ?? ex.Message}");
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

        private async Task<bool> ParkingZoneExists(int id)
        {
            return await _parkingZoneService.GetByIdAsync(id) != null;
        }
    }
}
