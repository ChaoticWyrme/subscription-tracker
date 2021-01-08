using Microsoft.AspNetCore.Mvc;
using SubWatchApi.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubWatchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly SubWatchContext _context;
        public SubscriptionController(SubWatchContext context)
        {
            _context = context;
        }

        [HttpGet("{username}/Subscribed")]
        public async Task<List<ServiceInfo>> GetSubscribedServices(string username)
        {
            return await _context.GetSubscribedServices(username).ToListAsync();
        }

        [HttpGet("{username}/Subscriptions")]
        public async Task<List<Subscription>> GetSubscriptions(string username)
        {
            return await _context.Subscriptions.Where(sub => sub.Username == username).ToListAsync();
        }

        [HttpGet("{id}/{username}")]
        public async Task<ActionResult<Subscription>> GetSubscription(long id, string username)
        {
            var subscription = await _context.Subscriptions.FindAsync(new { username, id });
            if (subscription == null) return NotFound();
            return subscription;
        }

        [HttpGet("{id}/Subscribe")]
        public async Task<ActionResult<Subscription>> Subscribe(long id, [FromQuery] string username, [FromQuery] DateTime expireDate)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return NotFound("Service not found");
            var subscription = new Subscription
            {
                Username = username,
                ServiceId = id,
                ExpirationDate = expireDate,
            };
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return AcceptedAtAction(nameof(GetSubscription), new { id = subscription.ServiceId, username = subscription.Username }, subscription);
        }

        [HttpGet("{id}/Unsubscribe")]
        public async Task<ActionResult<List<Subscription>>> Unsubscribe(long id, [FromQuery] string username)
        {
            var subscription = await _context.Subscriptions.FindAsync(new { username, id });
            if (subscription == null) return NotFound();
            _context.Subscriptions.Remove(subscription);

            await _context.SaveChangesAsync();
            return NoContent();
        }
        
    }
}
