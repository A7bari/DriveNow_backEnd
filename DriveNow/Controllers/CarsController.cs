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
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace DriveNow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly DriveNowContext _context;

        public CarsController(DriveNowContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet("{page}")]

        public async Task<ActionResult<List<Car>>> GetCars(int page)
        {
            if(_context.Car == null)
            {
                return NotFound();
            }

            var pageResults = 10f;
            var pageCount = Math.Ceiling(_context.Car.Count() / pageResults);

            var cars = await _context.Car
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var response = new CarResponse
            {
                Cars = cars,
                CurrentPage = page,
                Pages = (int)pageCount,
                CarsCount = _context.Car.Count()
            };
            return Ok(response);

        }

        // GET: api/Cars/5
        [HttpGet("GetCar{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Car.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }
        [HttpGet("Search/{searchText}")]
        // CHERCHER PRODUIT 
        public async Task<ActionResult<List<Car>>> Search(string searchText)
        {
            return await _context.Car
                .Where(c => c.Brand.Contains(searchText) || c.Description.Contains(searchText) || c.Color.Contains(searchText) )
                .ToListAsync();
        } 

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutCar{id}")]
        public async Task<ActionResult<Car>> PutCar(int id, CarDto newCar)
        {
            Car car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            else
            {
                car.Id = id;
                car.Km = newCar.Km;
                car.LocationPrice = newCar.LocationPrice;
                car.Brand = newCar.Brand;
                car.Color = newCar.Color;
                car.Description = newCar.Description;
                car.Price= newCar.Price;
                car.ProductionYear= newCar.ProductionYear;
                car.FuelType= newCar.FuelType;

                _context.Entry(car).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(car);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }

       
        }

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(CarDto request)
        {
            Car car = new()
            {
                Price = request.Price,
                Brand = request.Brand,
                Color = request.Color,
                FuelType = request.FuelType,
                Km = request.Km,
                Description = request.Description,
                LocationPrice = request.LocationPrice,
                ProductionYear = request.ProductionYear
            };
            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.Id == id);
        }
    }
}
