using SignalRServer.Utils;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Check redis connection 
//TODO
//ConnectionMultiplexer redis;
//try
//{
//    redis = ConnectionMultiplexer.Connect(
//                new ConfigurationOptions
//                {
//                    EndPoints = { "localhost:6379" },
//                });

//    Console.WriteLine("[REDIS] connected to 'localhost:6379'");
//}
//catch (Exception)
//{
//    Console.WriteLine("[REDIS] Failed to connect to 'localhost:6379'");
//}

//SignalR declaration
builder.Services.AddSignalR();
       //TOTO.AddStackExchangeRedis("10.110.185.23", options => { options.Configuration.AbortOnConnectFail = false; }); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//SignalR mapping
app.MapHub<ChatHub>("/chatHub", options => {
    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
});

app.Run();
