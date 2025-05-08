using GoTorz.Models.Chat;
using GoTorz.Services;
using Microsoft.AspNetCore.SignalR;

namespace GoTorz.Hubs;

public class ChatHub : Hub
{
    private readonly MessageService _service;

    public ChatHub(MessageService store)
    {
        _service = store;
    }

    public async Task SendMessage(string userId, string userName, string message)
    {
        Console.WriteLine($"Message from {userName}: {message}");

        var msg = new ChatMessage
        {
            UserId = userId,
            UserName = userName,
            Message = message,
            TimeStamp = DateTime.UtcNow
        };

        await _service.AddAsync(msg);
        await Clients.All.SendAsync("ReceiveMessage", userName, message);
    }

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"{Context.ConnectionId} connected");

        var history = await _service.GetHistoryAsync();

        await Clients.Caller.SendAsync("LoadHistory", history);

        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        Console.WriteLine($"Disconnected {Context.ConnectionId} (error: “{exception?.Message ?? "none"}”)");
        return base.OnDisconnectedAsync(exception);
    }
}
