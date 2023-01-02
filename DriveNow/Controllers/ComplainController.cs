using DriveNow.Data;
using DriveNow.Dtos;
using DriveNow.Helpers;
using DriveNow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Controllers
{
    public class ComplainController:ControllerBase
    {


        private readonly DriveNowContext _context;
        private readonly IConfiguration _configuration;

        public ComplainController(IConfiguration configuration, DriveNowContext context)
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


        [HttpGet("AllComplains")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Complain>>> getAll()
        {
            var complains = _context.Complain.ToList();
            return Ok(complains);
        }
    }
}
