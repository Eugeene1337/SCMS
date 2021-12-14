using Microsoft.AspNetCore.Mvc;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using SCMS.API.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace SCMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ISubscriptionsRepository _subscriptionRepository;

        public SubscriptionsController(ISubscriptionService subscriptionService, ISubscriptionsRepository subscriptionRepository)
        {
            _subscriptionService = subscriptionService;
            _subscriptionRepository = subscriptionRepository;
        }

        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<Subscription>> GetSubscription(string userId)
        {
            
            return _subscriptionService.FindActiveSubscriptions(userId);
        }

        [HttpDelete("{subscriptionId}")]
        public IActionResult DeleteSubscription(Guid subscriptionId)
        {

            var subscription = _subscriptionRepository.GetSingle(subscriptionId);

            if (subscription == null)
            {
                return NotFound();
            }

            _subscriptionService.DectivateSubscription(subscriptionId);

            return Ok();
        }
    }
}
