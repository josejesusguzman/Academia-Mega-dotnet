using Grpc.Core;
using GrpcChat;

namespace ChatServer.Services;

public class ChatServiceImp : ChatService.ChatServiceBase
{
    private readonly ChatHub _hub;
    private readonly ILogger<ChatServiceImp> _log;

    public ChatServiceImp(ChatHub hub, ILogger<ChatServiceImp> log)
        => (_hub, _log) = (hub, log);

    public override async Task Chat(IAsyncStreamReader<ChatMessage> requestStream,
                                    IServerStreamWriter<ChatMessage> responseStream,
                                    ServerCallContext context)
    {
        if (!await requestStream.MoveNext())
            return;

        var first = requestStream.Current;
        var user = first.User;
        _hub.Join(user, responseStream);

        _log.LogInformation("{user} se ha conectado", user);
        await _hub.BroadcastAsync(first, context.CancellationToken);

        try
        {
            await foreach (var msg in requestStream.ReadAllAsync(context.CancellationToken))
            {
                _log.LogInformation("{u}: {t}", msg.User, msg.Text);
                await _hub.BroadcastAsync(msg, context.CancellationToken); 
            }
        }
        finally
        {
            _hub.Leave(user);
            _log.LogInformation("{user} se ha desconectado", user);
        }

    }
}