using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubWatchApi.Models;

namespace SubWatchApi.Controllers
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
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceInfo>> GetServiceById(long id)
        {
            var serviceInfo = await _context.Services.FindAsync(id);

            if (serviceInfo == null)
            {
                return NotFound();
            }

            return serviceInfo;
        }

        [HttpGet("MassGet")]
        public async Task<List<ActionResult<ServiceInfo>>> GetServicesById([FromQuery] long[] idList)
        {
            var results = new List<ActionResult<ServiceInfo>>();
            foreach (long id in idList)
            {
                results.Add(await GetServiceById(id));
            }
            return results;
        }

        // GET: api/Services?name=Netflix
        [HttpGet("Search")]
        public IEnumerable<ServiceInfo> SearchServices([FromQuery] ServiceSearchParameters searchParams)
        {
            Console.WriteLine("Search Parameters: ");
            DebugPrint.PrintProperties(searchParams);
            return searchParams.Search(_context.Services);
        }

        //[HttpGet("{username}/Search")]
        //public IEnumerable<ServiceInfo> 

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceInfo(long id, ServiceInfo serviceInfo)
        {
            if (id != serviceInfo.Id)
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
                if (!ServiceInfoExists(id))
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
        public async Task<ActionResult<ServiceInfo>> PostServiceInfo([FromBody] ServiceInfo serviceInfo)
        {
            if (serviceInfo.IconUrl == null) serviceInfo.IconUrl = "";
            if (serviceInfo.Url == null) serviceInfo.Url = "";
            if (serviceInfo.Name == null) return BadRequest();
            _context.Services.Add(serviceInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServiceById), new { id = serviceInfo.Id }, serviceInfo);
        }

        [HttpPost("MassCreate")]
        public async Task<ActionResult<List<ServiceInfo>>> MassPostServiceInfo([FromBody] List<ServiceInfo> serviceInfoList)
        {
            var results = new List<ServiceInfo>();
            var failures = new List<ServiceInfo>();
            foreach (ServiceInfo serviceInfo in serviceInfoList)
            {
                if (serviceInfo.IconUrl == null) serviceInfo.IconUrl = "";
                if (serviceInfo.Url == null) serviceInfo.Url = "";
                if (serviceInfo.Name == null)
                {
                    failures.Add(serviceInfo);
                } else
                {
                    results.Add(serviceInfo);
                }
                _context.Services.Add(serviceInfo);
            }
            await _context.SaveChangesAsync();

            if (failures.Count > 0)
            {
                return BadRequest(new {
                    message =  "Invalid service(s)",
                    invalidRequests = failures
                });
            }

            if (results.Count == 0) return BadRequest("no objects provided");

            return CreatedAtAction(nameof(GetServicesById), new { idList = results.Select(service => service.Id).ToArray() }, results);
        }

        // DELETE: api/ServiceInfos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceInfo(long id)
        {
            var serviceInfo = await _context.Services.FindAsync(id);
            if (serviceInfo == null)
            {
                return NotFound();
            }

            _context.Services.Remove(serviceInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceInfoExists(long id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
