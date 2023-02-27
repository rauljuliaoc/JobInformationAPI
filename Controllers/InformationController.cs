using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobInformationAPI.Data;
using JobInformationAPI.Models;

namespace JobInformationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly TodoDBContext _context;

        public InformationController(TodoDBContext context)
        {
            _context = context;
        }

        // GET: api/Information
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Information>>> GetInformation()
        {
          if (_context.Information == null)
          {
              return NotFound();
          }
            return await _context.Information.ToListAsync();
        }

        // GET: api/Information/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Information>> GetInformation(int id)
        {
          if (_context.Information == null)
          {
              return NotFound();
          }
            var information = await _context.Information.FindAsync(id);

            if (information == null)
            {
                return NotFound();
            }

            return information;
        }

        // PUT: api/Information/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformation(int id, Information information)
        {
            if (id != information.Id)
            {
                return BadRequest();
            }

            _context.Entry(information).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InformationExists(id))
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

        // POST: api/Information
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Information>> PostInformation(Information information)
        {
          if (_context.Information == null)
          {
              return Problem("Entity set 'TodoDBContext.Information'  is null.");
          }
            _context.Information.Add(information);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInformation", new { id = information.Id }, information);
        }

        // DELETE: api/Information/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformation(int id)
        {
            if (_context.Information == null)
            {
                return NotFound();
            }
            var information = await _context.Information.FindAsync(id);
            if (information == null)
            {
                return NotFound();
            }

            _context.Information.Remove(information);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetUserByCountryId/{countryId}")]
        public async Task<ActionResult<IEnumerable<Information>>> GetUserByCountryId(int countryId)
        {
            if(_context.Information== null)
            {
                return NotFound();
            }

            var information = await _context.Information
                .Include(i => i.Country)
                .Select(i => new Information
                {
                    Name = i.Name,
                    Id = i.Id,
                    CountryId = i.CountryId,
                    Country=  new CountryInfo { CountryName = i.Country.CountryName},
                })
                .Where(i => i.CountryId == countryId)
                .ToListAsync();

            return information == null ? NotFound() : information;
        }


        [HttpGet("GetUsersByJobId/{jobId}")]
        public async Task<ActionResult<IEnumerable<Information>>> GetUsersByJobId(int jobId)
        {
            if (_context.Information == null)
            {
                return NotFound();
            }

            var information = await _context.Information
                .Include(i => i.Job)
                .Select(i => new Information
                {
                    Name = i.Name,
                    Id = i.Id,
                    JobId = i.JobId,
                    Job = new JobInformation { JobName = i.Job.JobName},
                })
                .Where(i => i.JobId == jobId)
                .ToListAsync();

            return information == null ? NotFound() : information;
        }

        private bool InformationExists(int id)
        {
            return (_context.Information?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
