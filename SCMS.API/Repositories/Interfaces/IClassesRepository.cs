using SCMS.API.Models;
using System.Collections.Generic;

namespace SCMS.API.Repositories.Interfaces
{
    public interface IClassesRepository
    {
        Class GetSingle(int id);
        List<Class> GetAll();
        void Add(Class item);
        void Update(Class item);
        void Delete(int id);
        public bool Save();
    }
}
