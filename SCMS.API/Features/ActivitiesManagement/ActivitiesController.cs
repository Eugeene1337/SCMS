using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCMS.API.Data;
using SCMS.API.Features.ActivitiesManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCMS.API.Features.ActivitiesManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
        {
            return await _context.Activities
                .Select(x => new Activity()
                {
                    ActivityId = x.ActivityId,
                    ActivityName = x.ActivityName,
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            return activity;
        }

        [HttpPost]
        public async Task<ActionResult<Activity>> PostActivity([FromBody] Activity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(int id,  Activity activity)
        {
            activity.ActivityId = id;
            _context.Entry(activity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Activity>> DeleteActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ActivityExists(int id)
        {
            return _context.Activities.Any(e => e.ActivityId == id);
        }
    }
}
