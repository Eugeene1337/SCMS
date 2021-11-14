using SCMS.API.Data;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SCMS.API.Repositories
{
    public class PacketsRepository : IPacketsRepository
    {
        private readonly ApplicationDbContext _context;

        public PacketsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Packet> GetAll() => _context.Packets.ToList();

        public Packet GetSingle(int id) => _context.Packets.Find(id);

        public void Add(Packet item)
        {
            _context.Packets.Add(item);
        }

        public void Update(Packet item)
        {
            _context.Packets.Update(item);
        }

        public void Delete(int id)
        {
            Packet packet = GetSingle(id);
            _context.Packets.Remove(packet);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
