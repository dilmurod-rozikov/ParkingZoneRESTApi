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
    public class ParkingZonesController : ControllerBase
    {
        private readonly IParkingZoneService _parkingZoneService;
        private readonly IMapper _mapper;

        public ParkingZonesController(IParkingZoneService parkingZoneService, IMapper mapper)
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingZoneDto>>> SearchByTitleAndAddress(string title, string address)
        {
            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(address))
                return BadRequest("Title and address cannot both be null or empty.");

                List<ParkingZone> result = [];

            if (!string.IsNullOrWhiteSpace(title))
                result.AddRange(await _parkingZoneService.SearchByTitle(title));

            if (!string.IsNullOrWhiteSpace(address))
                result.AddRange(await _parkingZoneService.SearchByAddress(address));

            if (result.Count == 0)
                return NotFound($"Not a single parking-zone exist with {title} or {address}");

            var map = _mapper.Map<IEnumerable<ParkingZoneDto>>(result);
            return Ok(map);
        }

        [HttpPost]
        public async Task<ActionResult<ParkingZone>> CreateParkingZone(ParkingZoneDto zoneDto)
        {
            if (zoneDto is null)
                return NotFound();
            var zones = await _parkingZoneService.GetAllAsync();

            if (_parkingZoneService.HasUniqueTitleAndAddress(zones, zoneDto.Title, zoneDto.Address))
            {
                ModelState.AddModelError("", "Parkingzone with this title and address alreay exist.");
                return BadRequest("Parkingzone with this title and address alreay exist.");
            }

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

            var zones = await _parkingZoneService.GetAllAsync();
            if (_parkingZoneService.HasUniqueTitleAndAddress(zones, zoneDto.Title, zoneDto.Address))
            {
                ModelState.AddModelError("", "Parkingzone with this title and address alreay exist.");
                return BadRequest("Parkingzone with this title and address alreay exist.");
            }

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
