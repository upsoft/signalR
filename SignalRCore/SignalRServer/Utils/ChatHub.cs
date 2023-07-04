using Microsoft.AspNetCore.SignalR;

namespace SignalRServer.Utils
{
    public class ChatHub: Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"[{Environment.MachineName}] {user}", message);
        }
    }
}
