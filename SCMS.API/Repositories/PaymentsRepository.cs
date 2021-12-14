using SCMS.API.Data;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCMS.API.Repositories
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Payment> GetAll() => _context.Payments.ToList();

        public Payment GetSingle(int id) => _context.Payments.Find(id);

        public void Add(Payment item)
        {
            item.DateTime = DateTime.Now;
            _context.Payments.Add(item);
        }

        public void Update(Payment item)
        {
            _context.Payments.Update(item);
        }

        public void Delete(int id)
        {
            Payment payment = GetSingle(id);
            _context.Payments.Remove(payment);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
