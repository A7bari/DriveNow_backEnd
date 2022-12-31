using Azure.Core;
using DriveNow.Data;
using DriveNow.Dtos;
using DriveNow.Helpers;
using DriveNow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Security.Cryptography;

namespace DriveNow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DriveNowContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration, DriveNowContext context)
        {
            _configuration = configuration;
            _context = context;
        }

      

        // POST: api/Auth/register/admin
        [HttpPost("register/admin")]
        public async Task<ActionResult<User>> RegisterAdmin(AdminRegisterDto request)
        {
            if (GetUserByEmail(request.Email) != null)
                return BadRequest("User already exist.");

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new Admin()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Roles.Admin
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // POST: api/Auth/register/owner
        [HttpPost("register/owner")]
        public async Task<ActionResult<User>> RegisterOwner(OwnerRegisterDto request)
        {
            if (GetUserByEmail(request.Email) != null)
                return BadRequest("User already exist.");

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new Owner()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Roles.Owner,
                CIN = request.CIN,
                Adress = request.Adress,
                HasAgancy = request.HasAgancy

            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // POST: api/Auth/register/tenant
        [HttpPost("register/tenant")]
        public async Task<ActionResult<User>> RegisterTenant(TenantRegiaterDto request)
        {
            if (GetUserByEmail(request.Email) != null)
                return BadRequest("User already exist.");

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new Tenant()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Roles.Tenant,
                 CIN = request.CIN
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // Put: api/Auth/login
        [HttpPut("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserLoginDto request)
        {
            User user =  GetUserByEmail(request.Email);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            return Ok(new
            {
                user,
                token
            });
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private User GetUserByEmail(string email)
        {

            return _context.User.FirstOrDefault(u => u.Email.Equals(email));

        }
    }
}
