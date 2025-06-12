using Grpc.Net.Client;
using Grpc.Core;
using GrpcChat;


// Setup del chat
Console.Write("¿Cuál es tu nombre?");
var user = Console.ReadLine() ?? "annonymus";

using var channel = GrpcChannel.ForAddress("http://localhost:5155", 
    new GrpcChannelOptions { UnsafeUseInsecureChannelCallCredentials = true });

var client = new ChatService.ChatServiceClient(channel);

// 1 Abrir el Stream
using var call = client.Chat();

// 2 Lanzar una atrea para escuchar los mensajes
var readTask = Task.Run(async () =>
{
    await foreach (var incoming in call.ResponseStream.ReadAllAsync())
    {
        var timeSend = DateTimeOffset.FromUnixTimeMilliseconds(incoming.Timestamp)
            .ToLocalTime().ToString("HH:mm:ss");

        Console.ForegroundColor = incoming.User == user ? ConsoleColor.Cyan : ConsoleColor.Yellow;
        Console.WriteLine($"[{timeSend}] {incoming.User}: {incoming.Text}");
        Console.ResetColor();

    }
});

// Hacer el join
await call.RequestStream.WriteAsync(new ChatMessage
{
    User = user,
    Text = $"{user} se ha unido al chat",
    Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
});


// Enviar los mensajes
string? line;

while (!string.IsNullOrEmpty(line = Console.ReadLine()))
{
    await call.RequestStream.WriteAsync(new ChatMessage
    {
        User = user,
        Text = line,
        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
    });
}

await call.RequestStream.CompleteAsync();
await readTask;


