using SCMS.API.Data;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCMS.API.Repositories
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Subscription> GetAll()
        {
            var subs = _context.Subscriptions.ToList();
            foreach (var sub in subs)
            {
                if (DateTime.Now > sub.ValidTo)
                {
                    sub.IsActive = false;
                    Update(sub);
                }
            }

            return subs;
        }

        public Subscription GetSingle(Guid id)
        {
            var sub = _context.Subscriptions.Find(id);
            if (DateTime.Now > sub.ValidTo)
            {
                sub.IsActive = false;
                Update(sub);
            }

            return sub;
        }

        public Subscription GetLastSubscription()
        {
            var subscriptions = GetAll();
            var orderedSubs = from sub in subscriptions
                               orderby sub.Created descending
                               select sub;

            return orderedSubs.FirstOrDefault();
        }
        public void Add(Subscription item)
        {
            item.Created = DateTime.Now;
            _context.Subscriptions.Add(item);
        }

        public void Update(Subscription item)
        {
            item.Modified = DateTime.Now;
            _context.Subscriptions.Update(item);
        }

        public void Delete(Guid id)
        {
            Subscription subscription = GetSingle(id);
            _context.Subscriptions.Remove(subscription);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
