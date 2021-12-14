using SCMS.API.Data;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using System;
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

        public List<Class> GetClassesWeek(string userId)
        {
            var classes = _context.Classes;
            var classEnrollments = _context.ClassEnrollments;
            var result = from clas in classes
                         join classEnrollment in classEnrollments on clas.ClassId equals classEnrollment.ClassId
                         where classEnrollment.UserId == userId && clas.DateTime.Date.CompareTo(DateTime.Now.Date) >= 0 && clas.DateTime.Date.CompareTo(DateTime.Now.Date.AddDays(7)) == -1
                         select clas;

            return result.ToList();
        }

        public void Add(Class item)
        {
            _context.Classes.Add(item);
        }

        public void Add(ClassEnrollment item)
        {
            _context.ClassEnrollments.Add(item);
        }

        public void Delete(ClassEnrollment item)
        {
            _context.ClassEnrollments.Remove(item);
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

        public List<Class> GetClasses(string userId)
        {
            var classes = _context.Classes;
            var classEnrollments = _context.ClassEnrollments;
            var result = from clas in classes
                         join classEnrollment in classEnrollments on clas.ClassId equals classEnrollment.ClassId
                         where classEnrollment.UserId == userId
                         select clas;

            return result.ToList();
        }

        public List<User> GetEnrolledUsers(int id)
        {
            var users = _context.Users;
            var classEnrollments = _context.ClassEnrollments;
            var result = from u in users
                         join classEnrollment in classEnrollments on u.Id equals classEnrollment.UserId
                         where classEnrollment.ClassId == id
                         select u;

            return result.ToList();
        }
    }
}
