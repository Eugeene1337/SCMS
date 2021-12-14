using SCMS.API.Data;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCMS.API.Repositories
{
    public class AnnouncementsRepository : IAnnouncementsRepository
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Announcement> GetAll() => _context.Announcements.ToList();

        public Announcement GetSingle(int id) => _context.Announcements.Find(id);

        public void Add(Announcement item)
        {
            item.Created = DateTime.Now;
            _context.Announcements.Add(item);
        }

        public void Update(Announcement item)
        {
            item.Modified = DateTime.Now;
            _context.Announcements.Update(item);
        }

        public void Delete(int id)
        {
            Announcement announcement = GetSingle(id);
            _context.Announcements.Remove(announcement);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
