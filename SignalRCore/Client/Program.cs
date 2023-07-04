// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;

//Set SignalR connection
var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5051/chatHub", options=>
                //.WithUrl("http:/signal.localhost/chatHub", options=>
                {
                    options.SkipNegotiation = true;
                    options.Transports = HttpTransportType.WebSockets;
                }).Build();

//Close event
connection.Closed += async (error) =>
{
    await Task.Delay(new Random().Next(0, 5) * 1000);
    await connection.StartAsync();
};

connection.On<string, string>("ReceiveMessage", (user, message) =>
{
    Console.WriteLine($"{user}: {message}");
    //this.Dispatcher.Invoke(() =>
    //{
    //    var newMessage = $"{user}: {message}";
    //    messagesList.Items.Add(newMessage);
    //});
});

try
{
    await connection.StartAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("Chat is started ! (enter 'q' for exit)");

var msg = "";
do
{
    msg = Console.ReadLine();
    if (msg != "q")
    {
        await connection.InvokeAsync("SendMessage", Environment.MachineName, msg);
    }

} while (msg != "q");