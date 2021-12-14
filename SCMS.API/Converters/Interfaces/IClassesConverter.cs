using SCMS.API.DTO;
using SCMS.API.Models;
using System.Collections.Generic;

namespace SCMS.API.Converters.Interfaces
{
    public interface IClassesConverter
    {
        List<GetClassDto> Convert(IEnumerable<Class> classes);
    }
}
