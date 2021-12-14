using SCMS.API.Data;
using SCMS.API.DTO;
using SCMS.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCMS.API.Services.Interfaces
{
    public class ClassesReportService : IClassesReportService
    {
        private readonly IClassesRepository _classesRepository;

        private readonly ApplicationDbContext _context;

        public ClassesReportService(IClassesRepository classesRepository, ApplicationDbContext context)
        {
            _classesRepository = classesRepository;
            _context = context;
        }

        public List<Report> GetReport(string userId)
        {
            List<Report> reports = new List<Report>();
            int dayGap = GetDayGap();
            DateTime startDate = DateTime.Today.AddDays(dayGap);
            DateTime endDate = startDate.AddDays(-7);
            var classes = _classesRepository.GetClasses(userId);

            
            for(int i = 0; i < 6; i++)
            {
                var result = from clas in classes
                             where clas.DateTime <= startDate && clas.DateTime >= endDate
                             select clas;

                var report = new Report()
                {
                    Week = i,
                    Count = result.Count()
                };

                reports.Add(report);

                startDate = endDate;
                endDate = startDate.AddDays(-7);
            }

            return reports;

        }

        private int GetDayGap()
        {
            return DateTime.Today.DayOfWeek switch
            {
                DayOfWeek.Monday => 6,
                DayOfWeek.Tuesday => 5,
                DayOfWeek.Wednesday => 4,
                DayOfWeek.Thursday => 3,
                DayOfWeek.Friday => 2,
                DayOfWeek.Saturday => 1,
                DayOfWeek.Sunday => 0,
                _ => 0,
            };
        }
    }
}
