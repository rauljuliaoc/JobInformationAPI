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
    public class JobInformationsController : ControllerBase
    {
        private readonly TodoDBContext _context;

        public JobInformationsController(TodoDBContext context)
        {
            _context = context;
        }

        // GET: api/JobInformations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobInformation>>> GetJobInformations()
        {
          if (_context.JobInformations == null)
          {
              return NotFound();
          }
            return await _context.JobInformations.ToListAsync();
        }

        // GET: api/JobInformations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobInformation>> GetJobInformation(int id)
        {
          if (_context.JobInformations == null)
          {
              return NotFound();
          }
            var jobInformation = await _context.JobInformations.FindAsync(id);

            if (jobInformation == null)
            {
                return NotFound();
            }

            return jobInformation;
        }

        // PUT: api/JobInformations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobInformation(int id, JobInformation jobInformation)
        {
            if (id != jobInformation.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobInformationExists(id))
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

        // POST: api/JobInformations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobInformation>> PostJobInformation(JobInformation jobInformation)
        {
          if (_context.JobInformations == null)
          {
              return Problem("Entity set 'TodoDBContext.JobInformations'  is null.");
          }
            _context.JobInformations.Add(jobInformation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobInformation", new { id = jobInformation.Id }, jobInformation);
        }

        // DELETE: api/JobInformations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobInformation(int id)
        {
            if (_context.JobInformations == null)
            {
                return NotFound();
            }
            var jobInformation = await _context.JobInformations.FindAsync(id);
            if (jobInformation == null)
            {
                return NotFound();
            }

            _context.JobInformations.Remove(jobInformation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobInformationExists(int id)
        {
            return (_context.JobInformations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
