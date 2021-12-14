using SCMS.API.Models;
using System;
using System.Collections.Generic;

namespace SCMS.API.Services.Interfaces
{
    public interface ISubscriptionService
    {
        List<Subscription> FindActiveSubscriptions(string userId);

        Subscription FindSubscription(string userId, int packetId);

        Subscription GetLastSubscription();

        void CreateSubscription(int packetId, string userId);

        void ActivateSubscription(Guid subscriptionId);

        void DectivateSubscription(Guid subscriptionId);

        void DeleteSubscription(Guid subscriptionId);
    }
}
