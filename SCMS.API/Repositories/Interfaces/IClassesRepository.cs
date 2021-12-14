using SCMS.API.Models;
using System.Collections.Generic;

namespace SCMS.API.Repositories.Interfaces
{
    public interface IClassesRepository
    {
        Class GetSingle(int id);
        List<Class> GetAll();
        List<Class> GetClassesWeek(string userId);
        List<Class> GetClasses(string userId);
        List<User> GetEnrolledUsers(int id);
        void Add(Class item);
        void Add(ClassEnrollment item);
        void Update(Class item);
        void Delete(int id);
        void Delete(ClassEnrollment item);
        public bool Save();
    }
}
