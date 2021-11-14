using SCMS.API.Models;
using System.Collections.Generic;

namespace SCMS.API.Repositories.Interfaces
{
    public interface IActivitiesRepository
    {
        Activity GetSingle(int id);
        List<Activity> GetAll();
        void Add(Activity item);
        void Update(Activity item);
        void Delete(int id);
        public bool Save();
    }
}
