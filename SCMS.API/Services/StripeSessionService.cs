using SCMS.API.Repositories.Interfaces;
using SCMS.API.Services.Interfaces;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.
    Tasks;

namespace SCMS.API.Services
{
    public class StripeSessionService : IStripeSessionService
    {
        private readonly IUsersRepository _usersRepository;

        public StripeSessionService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Session CreateSession(string priceId, string userId)
        {
            var user = _usersRepository.GetSingle(Guid.Parse(userId));
            var link = "https://stripe.com/";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = priceId,
                        Quantity = 1,
                    },
                },
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Mode = "subscription",
                SuccessUrl = link,
                CancelUrl = link,
                //Customer = userId,
                CustomerEmail = user?.Email
            };
            var service = new SessionService();
            Session session = service.Create(options);

            return session;
        }
    }
}
