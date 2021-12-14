using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using SCMS.API.Services.Interfaces;
using Stripe;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SCMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IStreamChatService _streamChatService;

        public WebhookController(UserManager<User> userManager, IPaymentsRepository paymentsRepository, ISubscriptionService subscriptionService, IStreamChatService streamChatService)
        {
            _userManager = userManager;
            _paymentsRepository = paymentsRepository;
            _subscriptionService = subscriptionService;
            _streamChatService = streamChatService;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var subscription = _subscriptionService.GetLastSubscription();
                    _subscriptionService.ActivateSubscription(subscription.SubscriptionId);
                    _streamChatService.CreateChannelWithTrainer(subscription.UserId);
                }
                else if (stripeEvent.Type == Events.PaymentIntentCanceled)
                {
                    var subscription = _subscriptionService.GetLastSubscription();
                    _subscriptionService.DectivateSubscription(subscription.SubscriptionId);
                }
                // ... handle other event types
                else
                {
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}
