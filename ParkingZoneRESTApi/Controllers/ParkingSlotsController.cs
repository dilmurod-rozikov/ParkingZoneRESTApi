using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingZoneWebApi.DTOs;
using ParkingZoneWebApi.Models;
using ParkingZoneWebApi.Services.Interfaces;

namespace ParkingZoneWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class ParkingSlotsController : ControllerBase
    {
        private readonly IParkingSlotService _parkingSlotService;
        private readonly IParkingZoneService _parkingZoneService;
        private readonly IMapper _mapper;

        public ParkingSlotsController(IParkingSlotService parkingSlotService, IParkingZoneService parkingZoneService, IMapper mapper)
        {
            _parkingSlotService = parkingSlotService;
            _parkingZoneService = parkingZoneService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingSlotDto>>> GetParkingSlots()
        {
            var slots = _mapper.Map<List<ParkingSlotDto>>(await _parkingSlotService.GetAllAsync());
            if (slots is null)
                return NotFound();

            return Ok(slots);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSlot>> GetParkingSlotById(int id)
        {
            var parkingSlot = _mapper.Map<ParkingSlotDto>(await _parkingSlotService.GetByIdAsync(id));

            if (parkingSlot == null)
                return NotFound();

            return Ok(parkingSlot);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateParkingSlot(int id, ParkingSlotDto parkingSlotDto)
        {
            if (id != parkingSlotDto.Id)
                return BadRequest();
            var slot = await _parkingSlotService.GetByIdAsync(parkingSlotDto.Id);
            var zone = await _parkingZoneService.GetByIdAsync(parkingSlotDto.ParkingZoneId);

            if (slot is null || zone is null)
                return NotFound("parkingslot or parkingzone is not found.");

            var mapped = _mapper.Map<ParkingSlot>(parkingSlotDto);

            try
            {
                await _parkingSlotService.UpdateAsync(mapped);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError("", $"Database Update Exception: {ex.InnerException?.Message ?? ex.Message}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateParkingSlot(ParkingSlotDto dto)
        {
            var zone = await _parkingZoneService.GetByIdAsync(dto.ParkingZoneId);
            if(dto is null || zone is null)
                return BadRequest();

            var mapped = _mapper.Map<ParkingSlot>(dto);
            mapped.ParkingZoneId = zone.Id; 

            try
            {
                await _parkingSlotService.CreateAsync(mapped);
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", $"Database Update Exception: {ex.InnerException?.Message ?? ex.Message}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
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