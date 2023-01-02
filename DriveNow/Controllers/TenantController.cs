using DriveNow.Data;
using DriveNow.Dtos;
using DriveNow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantController : ControllerBase
    {
        private readonly DriveNowContext _context;
        private readonly IConfiguration _configuration;

        public TenantController(IConfiguration configuration, DriveNowContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Tenant>> UpdateTenant(TenantRegiaterDto tenant, int id)
        {
            var _tenant = (Tenant)await _context.User.FindAsync(id);
            if (_tenant == null)
            {
                return NotFound();
            }
            _tenant.FirstName = tenant.FirstName;
            _tenant.LastName = tenant.LastName;
            _tenant.Email = tenant.Email;
            _tenant.CIN = tenant.CIN;

            await _context.SaveChangesAsync();
            return Ok(tenant);
        }
    }
}
