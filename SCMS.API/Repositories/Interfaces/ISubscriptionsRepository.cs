using SCMS.API.Models;
using System;
using System.Collections.Generic;

namespace SCMS.API.Repositories.Interfaces
{
    public interface ISubscriptionsRepository
    {
        Subscription GetSingle(Guid id);
        List<Subscription> GetAll();
        Subscription GetLastSubscription();
        void Add(Subscription item);
        void Update(Subscription item);
        void Delete(Guid id);
        public bool Save();
    }
}
