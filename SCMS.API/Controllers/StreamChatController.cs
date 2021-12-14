using Microsoft.AspNetCore.Mvc;
using SCMS.API.Models;
using SCMS.API.Services.Interfaces;
using StreamChat;

namespace SCMS.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StreamChatController : ControllerBase
    {
        private readonly IStreamChatService _streamChatService;

        public StreamChatController(IStreamChatService streamChatService)
        {
            _streamChatService = streamChatService;
        }

        [HttpGet("userId")]
        public IActionResult GetToken(string userId)
        {
            var token = _streamChatService.GetToken(userId);
            return Ok(token);
        }

        [HttpGet("userId")]
        public IActionResult CreateChannelWithTrainer (string userId)
        {
            _streamChatService.CreateChannelWithTrainer(userId);
            return Ok();
        }

        [HttpGet("userId")]
        public IActionResult AddToMainChannel(string userId)
        {
            _streamChatService.AddToMainChannel(userId);
            return Ok();
        }

        [HttpPost]
        public IActionResult UpateUserName(string userId, string name)
        {
            _streamChatService.UpdateUser(userId, name);
            return Ok();
        }
    }
}
