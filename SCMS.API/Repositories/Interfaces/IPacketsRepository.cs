using SCMS.API.Models;
using System.Collections.Generic;

namespace SCMS.API.Repositories.Interfaces
{
    public interface IPacketsRepository
    {
        Packet GetSingle(int id);
        List<Packet> GetAll();
        void Add(Packet item);
        void Update(Packet item);
        void Delete(int id);
        public bool Save();
    }
}
