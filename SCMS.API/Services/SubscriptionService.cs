using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using SCMS.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCMS.API.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public SubscriptionService(ISubscriptionsRepository subscriptionsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
        }

        public void CreateSubscription(int packetId, string userId)
        {
            var existingSubscription = FindSubscription(userId, packetId);

            if(existingSubscription != null)
            {
                throw new Exception($"subscription with parameters userId : [{userId}], packetId : [{packetId}] already exists!");
            }

            Subscription subscription = new Subscription()
            {
                PacketId = packetId,
                UserId = userId,
            };

            _subscriptionsRepository.Add(subscription);

            if (!_subscriptionsRepository.Save())
            {
                throw new Exception("Updating a subscription failed on save.");
            }
        }

        public Subscription GetLastSubscription()
        {
            return _subscriptionsRepository.GetLastSubscription();
        }

        public void ActivateSubscription(Guid subscriptionId)
        {
            var subscription = _subscriptionsRepository.GetSingle(subscriptionId);

            if (subscription == null)
            {
                throw new System.ArgumentNullException();
            }

            subscription.IsActive = true;

            _subscriptionsRepository.Update(subscription);
            
            if (!_subscriptionsRepository.Save())
            {
                throw new Exception("Updating a subscription failed on save.");
            }
        }

        public void DectivateSubscription(Guid subscriptionId)
        {
            var subscription = _subscriptionsRepository.GetSingle(subscriptionId);

            if (subscription == null)
            {
                throw new System.ArgumentNullException();
            }

            subscription.IsActive = false;
            subscription.ValidTo = DateTime.Today.AddMonths(1);
            subscription.ValidTo.AddDays(-subscription.ValidTo.Day+1);

            _subscriptionsRepository.Update(subscription);

            if (!_subscriptionsRepository.Save())
            {
                throw new Exception("Updating a subscription failed on save.");
            }
        }

        public void DeleteSubscription(Guid subscriptionId)
        {
            var subscription = _subscriptionsRepository.GetSingle(subscriptionId);

            if(subscription == null)
            {
                throw new System.ArgumentNullException();
            }

            _subscriptionsRepository.Delete(subscriptionId);

            if (!_subscriptionsRepository.Save())
            {
                throw new Exception("Deleting a subscrition failed on save.");
            }
        }

        public List<Subscription> FindActiveSubscriptions(string userId)
        {
            var subscriptions = _subscriptionsRepository.GetAll();
            var result = from s in subscriptions
                         where s.UserId == userId && s.IsActive
                         select s;

            return result.ToList();
        }

        public Subscription FindSubscription(string userId, int packetId)
        {
            var subscriptions = _subscriptionsRepository.GetAll();
            var result = from s in subscriptions
                         where s.UserId == userId && s.PacketId == packetId
                         select s;

            return result.FirstOrDefault();
        }
    }
}
