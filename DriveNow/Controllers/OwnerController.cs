using DriveNow.Data;
using DriveNow.Dtos;
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

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Owner>>> UpdateOwner (OwnerRegisterDto owner , int id )
        {
            var _owner = await _context.Owner.FindAsync(id);
            if (_owner == null)
            {
                return NotFound();
            }
            _owner.FirstName = owner.FirstName;
            _owner.LastName = owner.LastName;
            _owner.Email = owner.Email;
            _owner.Adress=owner.Adress;
            _owner.CIN = owner.CIN;
            if (_owner.HasAgancy)
            {
                _owner.Agency.Name = owner.agency.Name;
                _owner.Agency.Adress = owner.agency.Adress;
            }
            else {
                _owner.Agency = null;
            }
            await _context.SaveChangesAsync();
            return Ok(await _context.Owner.ToListAsync());
        }
    }
}