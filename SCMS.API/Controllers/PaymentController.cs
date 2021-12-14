using Microsoft.AspNetCore.Mvc;
using SCMS.API.DTO;
using SCMS.API.Repositories.Interfaces;
using SCMS.API.Services.Interfaces;
using System;

namespace SCMS.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPacketsRepository _packetsRepository;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IStripeSessionService _stripeSessionService;

        public PaymentController(
            IPacketsRepository packetsRepository,
            ISubscriptionService subscriptionService,
            IStripeSessionService stripeSessionService)
        {
            _packetsRepository = packetsRepository;
            _subscriptionService = subscriptionService;
            _stripeSessionService = stripeSessionService;
        }

        [HttpPost]
        public ActionResult<CheckoutModel> Checkout([FromBody] CreateCheckoutDto createCheckoutDto)
        {
            var packet = _packetsRepository.GetSingle(createCheckoutDto.PackietId);

            if(packet == null)
            {
                return NotFound();
            }

            var session = _stripeSessionService.CreateSession(packet.StripePriceId, createCheckoutDto.UserId);

            try
            {
                _subscriptionService.CreateSubscription(createCheckoutDto.PackietId, createCheckoutDto.UserId);
            }
            catch (Exception e)
            {
                return UnprocessableEntity(e.Message);
            }

            CheckoutModel checkout = new CheckoutModel()
            {
                PaymentLink = session.Url,
            };
            return Accepted(checkout);
        }
    }
}
