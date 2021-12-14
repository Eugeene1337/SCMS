using SCMS.API.Models;
using System.Collections.Generic;

namespace SCMS.API.Repositories.Interfaces
{
    public interface IPaymentsRepository
    {
        Payment GetSingle(int id);
        List<Payment> GetAll();
        void Add(Payment item);
        void Update(Payment item);
        void Delete(int id);
        public bool Save();
    }
}
