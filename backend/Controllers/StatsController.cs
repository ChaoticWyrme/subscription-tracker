using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubWatchApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SubWatchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private SubWatchContext _context;

        public StatsController(SubWatchContext context)
        {
            _context = context;
        }

        [HttpGet("TotalTime")]
        public ActionResult<TimeSpan> TotalTimeSite([FromQuery][Required] string url, [FromQuery][Required] string username)
        {
            var service = GetService(username, url);
            return _context.TimeDatas.Where(data => data.ServiceId == service.Id).AsEnumerable().Select(timeData => timeData.UsedTime).Aggregate((acc, val) => acc + val);
        }

        [HttpGet("AllSites/TotalTime")]
        public async Task<ActionResult<Dictionary<string, TimeSpan>>> TotalTimeAllSites([FromQuery][Required] string username)
        {
            var times = await _context.TimeDatas.Where(data => data.Username == username).GroupBy(data => data.ServiceId).ToListAsync();

            var dict = new Dictionary<string, TimeSpan>();

            foreach (TimeData time in times)
            {
                var timeSpan = TimeSpan.Zero;
                bool result = dict.TryGetValue(time.Service.Url, out timeSpan);
            }
            return new Dictionary<string, TimeSpan>();
        }

        private ServiceInfo GetService(string username, string url)
        {
            var result = _context.Subscriptions.Where(sub => sub.Username == username && sub.Service.Url == url).Select(sub => sub.Service).FirstOrDefault();
            if (result == null)
            {
                result =  _context.Services.Where(service => service.Url == url).FirstOrDefault();
            }
            return result;
        }
    }
}
