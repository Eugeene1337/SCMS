using Stripe.Checkout;

namespace SCMS.API.Services.Interfaces
{
    public interface IStripeSessionService
    {
        Session CreateSession(string priceId, string userId);
    }
}
