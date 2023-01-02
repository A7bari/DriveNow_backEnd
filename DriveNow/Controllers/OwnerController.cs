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
using DriveNow.Helpers;

namespace DriveNow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly DriveNowContext _context;

        public OwnerController(DriveNowContext context)
        {
            _context = context;
        }

        // GET: api/Owners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetOwner()
        {
            var user = _context.User.Where(u => u.Role == Roles.Owner).ToList();
            return Ok(user);
        }

        // GET: api/Owners/5
        /*    [HttpGet("{id}")]
            public async Task<ActionResult<Owner>> GetOwner(int id)
            {
                var owner = await _context.Owner.FindAsync(id);

                if (owner == null)
                {
                    return NotFound();
                }

                return owner;
            }*/

        // PUT: api/Owners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Owner>> PutOwner(int id, OwnerRegisterDto owner)
        {
            var _owner = (Owner)await _context.User.FindAsync(id);
            if (_owner == null)
            {
                return NotFound();
            }
            else
            {
                _owner.Id = id;
                _owner.LastName = owner.LastName;
                _owner.Email = owner.Email;
                _owner.Adress = owner.Adress;
                _owner.CIN = owner.CIN;
                /*  var agency = _context.Agency.FirstOrDefault(a => a.OwnerId == _owner.Id);
                  if (_owner.HasAgancy)
                  {
                      _owner.Agency.Name = owner.agency.Name;
                      _owner.Agency.Adress = owner.agency.Adress;
                //_context.Agency.Add(agency);
                  }
                  else
                  {
                      if (agency != null)
                          // _owner.Agency = null;
                          _context.Agency.Remove(agency);
                      // await _context.SaveChangesAsync();
                  }
  */

                //_context.Entry(owner).State = EntityState.Modified;



                if (_owner.HasAgancy && _owner.Agency!= null)
                {
                    _owner.Agency.Name = owner.agency.Name;
                    _owner.Agency.Adress = owner.agency.Adress;
                    
                    _context.Agency.Add(_owner.Agency);
                }
                else
                {
                    var agency = _context.Agency.FirstOrDefault(a => a.OwnerId == _owner.Id);
                    if (agency != null)
                    {
                        _context.Agency.Remove(agency);
                    }
                }
                _context.Entry(_owner).CurrentValues.SetValues(owner);
             await    _context.SaveChangesAsync();
                return Ok(_owner);
                /*     try
                     {
                         _context.Entry(_owner).CurrentValues.SetValues(owner);
                         _context.SaveChanges();
                         return Ok(_owner);
                     }
                     catch (DbUpdateConcurrencyException)
                     {
                         if (!OwnerExists(id))
                         {
                             return NotFound();
                         }
                         else
                         {
                             throw;
                         }
                     */
            }


            }

            
        }

       

       /* private bool OwnerExists(int id)
            {
                return _context.User.Any(e => e.Id == id);
            }
*/
       
    }
    
