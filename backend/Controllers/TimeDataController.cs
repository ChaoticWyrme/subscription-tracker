using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubWatchApi.Models;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace SubWatchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeDataController : ControllerBase
    {
        private readonly SubWatchContext _context;

        public TimeDataController(SubWatchContext context)
        {
            _context = context;
        }

        // GET: api/TimeData
        [HttpGet]
        public async Task<IEnumerable<TimeData>> GetAllTimeData()
        {
            return await _context.TimeDatas.ToListAsync();
        }

        [HttpGet("Id/{id}")]
        public async Task<ActionResult<TimeData>> GetById(long id)
        {
            return await _context.TimeDatas.FindAsync(id);
        }

        [HttpGet("Id/MassGet")]
        public async Task<ActionResult<List<TimeData>>> MassGetById(long[] idList)
        {
            var result = await _context.TimeDatas.Where(data => idList.Contains(data.Id)).ToListAsync();
            if (result.Count == 0)
            {
                return NotFound("No matches found");
            } else if (result.Count < idList.Length)
            {
                // some stuff didn't match
                return result;
            } else
            {
                // nothing didn't match
                return result;
            }
        }
        

        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<TimeData>>> GetTimeDataByUser(string username)
        {
            return await _context.TimeDatas.Where(data => data.Username == username).ToListAsync();
        }

        [HttpGet("{username}/{serviceName}")]
        public async Task<ActionResult<IEnumerable<TimeData>>> GetTimeDataByUsernameAndService(string username, string serviceName)
        {
            return await _context.TimeDatas.Where(data => data.Username == username && data.Service.Name == serviceName).ToListAsync();
        }

        [HttpPost("{username}")]
        public async Task<ActionResult<TimeData>> PostTimeData(string username, TimeDataExtensionDTO extensionData)
        {
            // TODO: Fix this to use subscription maybe
            var service = _context.Services.Where(service => service.Url == extensionData.Url).FirstOrDefault();
            if (service == null) return NotFound("Service matching url not found");
            var timeData = new TimeData
            {
                ServiceId = service.Id,
                ServiceName = service.Name,
                StartTime = extensionData.StartTime,
                EndTime = extensionData.EndTime,
                Username = username,
            };
            _context.TimeDatas.Add(timeData);
            await _context.SaveChangesAsync();
            return new CreatedResult("/api/TimeData", timeData);
        }


        [HttpPost("MassCreate/{username}")]
        public async Task<ActionResult<List<TimeData>>> MassCreate(string username, [FromBody] TimeDataExtensionDTO[] timeDatas)
        {
            // pre get all the services the user is subscribed to
            var services = _context.GetSubscribedServices(username).AsEnumerable();

            var results = new List<TimeData>();

            foreach (TimeDataExtensionDTO sourceData in timeDatas)
            {
                var service = services.Where(service => service.Url == sourceData.Url).FirstOrDefault();
                if (service == null) return BadRequest(new { message = "Malformed Time Data", badData = sourceData });
                TimeData timeData = new TimeData(username, sourceData, service);
                results.Add(timeData);
            }
            _context.TimeDatas.AddRange(results);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(MassGetById), new { idList = results.Select(timeData => timeData.Id).ToList() }, results);
        }
    }
}
