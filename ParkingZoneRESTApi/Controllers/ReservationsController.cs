﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingZoneWebApi.DTOs;
using ParkingZoneWebApi.Models;
using ParkingZoneWebApi.Services.Interfaces;

namespace ParkingZoneWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IParkingSlotService _parkingSlotService;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationsController(IParkingSlotService parkingSlotService,
            IReservationService reservationService, IMapper mapper)
        {
            _parkingSlotService = parkingSlotService;
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations()
        {
            var reservations = _mapper.Map<List<ReservationDto>>(await _reservationService.GetAllAsync());
            if (reservations is null)
                return NotFound();

            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetReservationById(int id)
        {
            var reservation = _mapper.Map<ReservationDto>(await _reservationService.GetByIdAsync(id));

            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpGet("slotId/{slotId}")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservationsBySlotId(int slotId)
        {
            var slot = await _parkingSlotService.GetByIdAsync(slotId);
            if (slot == null)
                return NotFound("Parkingslot does not exist with this id.");

            var reservations = _reservationService.GetReservationsBySlotId(slot);
            var mapped = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
            return Ok(mapped);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, ReservationDto dto)
        {
            if (id != dto.Id)
                return BadRequest();
            var slot = await _parkingSlotService.GetByIdAsync(dto.ParkingSlotId);
            if (slot is null)
                return BadRequest("Given ParkingSlot id is not Found");

            if (_parkingSlotService.IsFreeForReservation(slot, dto.Started, dto.Duration))
                return BadRequest("This slot is not free for specified time duration!!!");

            try
            {
                await _reservationService.UpdateAsync(_mapper.Map<Reservation>(dto));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError("", $"Database Update Exception: {ex.InnerException?.Message ?? ex.Message}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateReservation(ReservationDto dto)
        {
            var slot = await _parkingSlotService.GetByIdAsync(dto.ParkingSlotId);
            if (slot is null)
                return BadRequest("Given ParkingSlot id is not Found");

            if (_parkingSlotService.IsFreeForReservation(slot, dto.Started, dto.Duration))
                return BadRequest("This slot is not free for specified time duration!!!");

            if (dto.Started < DateTime.Now)
                return BadRequest("You can't reserve for past.");

            try
            {
                await _reservationService.CreateAsync(_mapper.Map<Reservation>(dto));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong while saving the data.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
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
