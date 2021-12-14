using SCMS.API.Models;
using System.Collections.Generic;

namespace SCMS.API.Repositories.Interfaces
{
    public interface IAnnouncementsRepository
    {
        Announcement GetSingle(int id);
        List<Announcement> GetAll();
        void Add(Announcement item);
        void Update(Announcement item);
        void Delete(int id);
        public bool Save();
    }
}
