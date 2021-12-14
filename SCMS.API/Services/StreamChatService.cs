using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using SCMS.API.Services.Interfaces;
using StreamChat;

namespace SCMS.API.Services
{
    public class StreamChatService : IStreamChatService
    {
        private readonly IStreamChatConfig _streamChatConfig;
        private readonly IUsersRepository _userRepository;
        private Client _client;

        public StreamChatService(IStreamChatConfig streamChatConfig, IUsersRepository userRepository)
        {
            _userRepository = userRepository;
            _streamChatConfig = streamChatConfig;
            _client = new Client(_streamChatConfig.ApiKey, _streamChatConfig.ApiSecret);
        }

        public string GetToken(string email)
        {
            return _client.CreateToken(email);
        }

        public void CreateChannelWithTrainer(string userId)
        {
            var trainerId = "cf0cb840-4c89-43c9-9ee9-221c353e212f";

            var chan = _client.Channel("messaging", $"{userId}-and-Trainer");
            chan.Create(userId, new string[] { userId, trainerId });

            System.Threading.Thread.Sleep(5000);

            var bobHi = new MessageInput()
            {
                Text = "Cześć!",
            };

            chan.SendMessage(bobHi, trainerId);
        }

        public void AddToMainChannel(string userId)
        {
            var chan = _client.Channel("messaging", "Main");
            chan.AddMembers(new string[] { userId });
        }

        public void UpdateUser(string userId, string name)
        {
            var bob = new StreamChat.User()
            {
                ID = userId,
            };
            bob.SetData("name", name);

            var bobFromDB = _client.Users.Upsert(bob);
        }
    }
}
