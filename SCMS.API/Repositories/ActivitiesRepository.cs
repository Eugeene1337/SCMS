using SCMS.API.Data;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SCMS.API.Repositories
{
    public class ActivitiesRepository : IActivitiesRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivitiesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Activity> GetAll() => _context.Activities.ToList();

        public Activity GetSingle(int id) => _context.Activities.Find(id);

        public void Add(Activity item)
        {
            _context.Activities.Add(item);
        }

        public void Update(Activity item)
        {
            _context.Activities.Update(item);
        }

        public void Delete(int id)
        {
            Activity activity = GetSingle(id);
            _context.Activities.Remove(activity);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
