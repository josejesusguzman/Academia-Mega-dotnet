using Grpc.Core;
using GrpcChat;

namespace ChatServer.Services;

public class ChatServiceImp : ChatService.ChatServiceBase
{
    private readonly ChatHub _hub;
    private readonly ILogger<ChatServiceImp> _log;

    public ChatServiceImp(ChatHub hub, ILogger<ChatServiceImp> log)
        => (_hub, _log) = (hub, log)
}