using DriveNow.Data;
using DriveNow.Dtos;
using DriveNow.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}