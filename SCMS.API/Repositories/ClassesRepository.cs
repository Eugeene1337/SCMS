using SCMS.API.Data;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SCMS.API.Repositories
{
    public class ClassesRepository : IClassesRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Class> GetAll() => _context.Classes.ToList();

        public Class GetSingle(int id) => _context.Classes.Find(id);

        public void Add(Class item)
        {
            _context.Classes.Add(item);
        }

        public void Update(Class item)
        {
            _context.Classes.Update(item);
        }

        public void Delete(int id)
        {
            Class classs = GetSingle(id);
            _context.Classes.Remove(classs);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
