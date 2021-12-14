using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCMS.API.Models
{
    public interface IStreamChatConfig
    {
        string ApiKey { get; set; }
        string ApiSecret { get; set; }
    }

    public class StreamChatConfig : IStreamChatConfig
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
}
