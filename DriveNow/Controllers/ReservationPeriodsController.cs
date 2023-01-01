using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DriveNow.Data;
using DriveNow.Models;
using DriveNow.Dtos;

namespace DriveNow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationPeriodsController : ControllerBase
    {
        private readonly DriveNowContext _context;

        public ReservationPeriodsController(DriveNowContext context)
        {
            _context = context;
        }

        // GET: api/ReservationPeriods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationPeriod>>> GetReservationPeriods()
        {
            return await _context.ReservationPeriods.ToListAsync();
        }

        // GET: api/ReservationPeriods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationPeriodDto>> GetReservationPeriod(int id)
        {
            var reservationPeriod = await _context.ReservationPeriods.FindAsync(id);
            var dto = new ReservationPeriodDto
            {
                StartDate = reservationPeriod.StartDate,
                EndDate = reservationPeriod.EndDate
            };

            if (reservationPeriod == null)
            {
                return NotFound();
            }

            return dto;
        }
    
        [HttpGet("CarId")]
        public async Task<ActionResult<List<ReservationPeriod>>> GetCarId(int carId)
        {
            var reservationPeriods = await _context.ReservationPeriods
                .Where(c => c.CarId == carId)
                .ToListAsync();
            return Ok(reservationPeriods);
        }

        // PUT: api/ReservationPeriods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservationPeriod(int id, ReservationPeriod reservationPeriod)
        {
            if (id != reservationPeriod.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservationPeriod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationPeriodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ReservationPeriods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReservationPeriod>> PostReservationPeriod(ReservationPeriod reservationPeriod)
        {
            _context.ReservationPeriods.Add(reservationPeriod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservationPeriod", new { id = reservationPeriod.Id }, reservationPeriod);
        }

        // DELETE: api/ReservationPeriods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservationPeriod(int id)
        {
            var reservationPeriod = await _context.ReservationPeriods.FindAsync(id);
            if (reservationPeriod == null)
            {
                return NotFound();
            }

            _context.ReservationPeriods.Remove(reservationPeriod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationPeriodExists(int id)
        {
            return _context.ReservationPeriods.Any(e => e.Id == id);
        }
    }
}
