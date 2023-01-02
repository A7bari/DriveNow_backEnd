using DriveNow.Data;
using DriveNow.Dtos;
using DriveNow.Helpers;
using DriveNow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Controllers
{
    public class OwnerController : ControllerBase
    {

        private readonly DriveNowContext _context;
        private readonly IConfiguration _configuration;

        public OwnerController(IConfiguration configuration, DriveNowContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("complain")]
        public async Task<IActionResult> ComplainSubmit(ComplainRegisterDto complain)
        {
            var user = await _context.User.FindAsync(complain.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var newComplain = new Complain()
            {
                Subject = complain.Subject,
                Message = complain.Message,
                date = complain.date,
                user = user
            };
            _context.Complain.Add(newComplain);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // GET: api/Owners
        [HttpGet("{page}")]

        public async Task<ActionResult<List<Owner>>> GetOwners(int page)
        {
            if (_context.User == null)
            {
                return NotFound();
            }

            var pageResults = 10f;
            var pageCount = Math.Ceiling(_context.User.Where(u => u.Role == Roles.Owner).Count() / pageResults);
            var owners = await _context.User.Where(u => u.Role == Roles.Owner).Include(o => o.cars).ToListAsync();

            
                owners = await _context.User
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

                var response = new ListResponse<User>
                {
                    elements = owners,
                    CurrentPage = page,
                    Pages = (int)pageCount,
                    elementsCount = _context.User.Where(u => u.Role == Roles.Owner).Count()
                };
                return Ok(response);

            }

        }
    }
