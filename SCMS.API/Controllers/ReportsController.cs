using Microsoft.AspNetCore.Mvc;
using SCMS.API.DTO;
using SCMS.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : Controller
    {
        private readonly IClassesReportService _classesReportService;

        public ReportsController(IClassesReportService classesReportService)
        {
            _classesReportService = classesReportService;
        }

        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<Report>> GetReport(string userId) => _classesReportService.GetReport(userId);
    }
}
