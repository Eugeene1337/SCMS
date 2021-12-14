namespace SCMS.API.Services.Interfaces
{
    public interface IStreamChatService
    {
        string GetToken(string userId);

        void AddToMainChannel(string userId);

        void CreateChannelWithTrainer(string userId);

        void UpdateUser(string userId, string name);
    }
}
