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
    public class TimeDataController
    {
        private readonly SubWatchContext _context;

        public TimeDataController(SubWatchContext context)
        {
            _context = context;
        }

        // GET: api/TimeData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeData>>> GetAllTimeData()
        {
            return await _context.TimeDatas.ToListAsync();
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<TimeData>>> GetTimeDataByUser(string username)
        {
            return await _context.TimeDatas.Where(data => data.Username == username).ToListAsync();
        }

        [HttpGet("{username}/{service}")]
        public async Task<ActionResult<IEnumerable<TimeData>>> GetTimeDataByUsernameAndService(string username, string serviceName)
        {
            return await _context.TimeDatas.Where(data => data.Username == username && data.Service.Name == serviceName).ToListAsync();
        }

        [HttpPost("{username}")]
        public async Task<ActionResult<TimeData>> PostTimeData(string username, TimeDataExtensionDTO extensionData)
        {
            var service = _context.Services.Where(service => service.Url == extensionData.Url).FirstOrDefault();
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
            return new CreatedResult("/", new { });
        }
    }
}
