using DriveNow.Data;
using DriveNow.Helpers;
using DriveNow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveNow.Controllers
{
    public class UserController:ControllerBase
    {   private readonly DriveNowContext _context;
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration, DriveNowContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            User _user = await _context.User.FindAsync(id);
            if (_user == null)
            {
                return BadRequest("Owner not found");
            }
            _context.User.Remove(_user);
            await _context.SaveChangesAsync();
            return Ok(_user);
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> GetAll()
        {
            return Ok( _context.User.ToList());
        }
    }
}
