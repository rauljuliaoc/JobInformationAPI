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
    public class CountryInfoesController : ControllerBase
    {
        private readonly TodoDBContext _context;

        public CountryInfoesController(TodoDBContext context)
        {
            _context = context;
        }

        // GET: api/CountryInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryInfo>>> GetCountryInfos()
        {
          if (_context.CountryInfos == null)
          {
              return NotFound();
          }
            return await _context.CountryInfos.ToListAsync();
        }

        // GET: api/CountryInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryInfo>> GetCountryInfo(int id)
        {
          if (_context.CountryInfos == null)
          {
              return NotFound();
          }
            var countryInfo = await _context.CountryInfos.FindAsync(id);

            if (countryInfo == null)
            {
                return NotFound();
            }

            return countryInfo;
        }

        // PUT: api/CountryInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryInfo(int id, CountryInfo countryInfo)
        {
            if (id != countryInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(countryInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryInfoExists(id))
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

        // POST: api/CountryInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CountryInfo>> PostCountryInfo(CountryInfo countryInfo)
        {
          if (_context.CountryInfos == null)
          {
              return Problem("Entity set 'TodoDBContext.CountryInfos'  is null.");
          }
            _context.CountryInfos.Add(countryInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountryInfo", new { id = countryInfo.Id }, countryInfo);
        }

        // DELETE: api/CountryInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryInfo(int id)
        {
            if (_context.CountryInfos == null)
            {
                return NotFound();
            }
            var countryInfo = await _context.CountryInfos.FindAsync(id);
            if (countryInfo == null)
            {
                return NotFound();
            }

            _context.CountryInfos.Remove(countryInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryInfoExists(int id)
        {
            return (_context.CountryInfos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
