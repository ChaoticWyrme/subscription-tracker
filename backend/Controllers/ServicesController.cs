using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubWatchApi.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly SubWatchContext _context;

        public ServicesController(SubWatchContext context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceInfo>>> GetServiceInfoList()
        {
            return await _context.Services.ToListAsync();
        }

        // GET: api/Services/5
        [HttpGet("{name}")]
        public async Task<ActionResult<ServiceInfo>> GetServiceInfo(long name)
        {
            var serviceInfo = await _context.Services.FindAsync(name);

            if (serviceInfo == null)
            {
                return NotFound();
            }

            return serviceInfo;
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{name}")]
        public async Task<IActionResult> PutServiceInfo(string name, ServiceInfo serviceInfo)
        {
            if (name != serviceInfo.Name)
            {
                return BadRequest();
            }

            _context.Entry(serviceInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceInfoExists(name))
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

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceInfo>> PostServiceInfo(ServiceInfo serviceInfo)
        {
            _context.Services.Add(serviceInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServiceInfo), new { name = serviceInfo.Name }, serviceInfo);
        }

        // DELETE: api/ServiceInfos/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteServiceInfo(string name)
        {
            var serviceInfo = await _context.Services.FindAsync(name);
            if (serviceInfo == null)
            {
                return NotFound();
            }

            _context.Services.Remove(serviceInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceInfoExists(string name)
        {
            return _context.Services.Any(e => e.Name == name);
        }
    }
}
