using SCMS.API.DTO;
using SCMS.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCMS.API.Services.Interfaces
{
    public interface IClassesReportService
    {
        List<Report> GetReport(string userId);
    }
}
