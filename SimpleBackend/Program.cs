using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

var redisConnectionString = builder.Configuration["ConnectionStrings:Redis"] ?? "localhost:6379";

try
{
    var redis = ConnectionMultiplexer.Connect(redisConnectionString);
    builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
}
catch
{
    Console.WriteLine($"WARNING: Could not connect to Redis at {redisConnectionString}. App will start but counter will fail.");
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

app.MapGet("/health", () => Results.Ok("Healthy"));

app.MapGet("/api/message", (IConnectionMultiplexer? muxer) =>
{
    var hostName = System.Net.Dns.GetHostName();
    long count = -1;

    if (muxer != null && muxer.IsConnected)
    {
        var db = muxer.GetDatabase();
        count = db.StringIncrement("page_views");
    }

    return Results.Ok(new
    {
        Message = "Hello from the Stateful Backend!",
        ServedBy = hostName,
        PageViewCount = count == -1 ? "Redis Unavailable" : count.ToString(),
        Timestamp = DateTime.UtcNow
    });
});

app.Run();
