using Microsoft.AspNetCore.SignalR;

namespace SignalRServer.Utils
{
    public class ChatHub: Hub
    {
        public override async  Task OnConnectedAsync()
        {
            try
            {
                //Get table id
                //User can not connect to hub without entering tabel room !
                string id = Context.GetHttpContext().Request.RouteValues["id"].ToString();

                if (!string.IsNullOrEmpty(id))
                {
                    //TODO - Check if user exist in table room !
                    var connectionId = Context.ConnectionId;
                    await Groups.AddToGroupAsync(connectionId, id);

                    await base.OnConnectedAsync();
                }
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                //log "User not entered to table"
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                string id = Context.GetHttpContext().Request.RouteValues["id"].ToString();

                var connectionId = Context.ConnectionId;
                await Groups.RemoveFromGroupAsync(connectionId, id);

                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception)
            {

                
            }
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"[{Environment.MachineName}] {user}", message);
        }
    }
}
