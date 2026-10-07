using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace CRMUNI.UI.WebSocket;

public class ChatHub : Hub
{
    // Mapeia nome de usuário → ConnectionId (em memória)
    private static readonly ConcurrentDictionary<string, string> _connections = new();

    public override Task OnDisconnectedAsync(System.Exception? exception)
    {
        // Remove o mapeamento quando a conexão é encerrada
        var entry = _connections.FirstOrDefault(kv => kv.Value == Context.ConnectionId);
        if (!string.IsNullOrEmpty(entry.Key))
        {
            // Só remove se o ConnectionId ainda corresponder ao usuário registrado
            _connections.TryRemove(entry.Key, out _);
        }
        return base.OnDisconnectedAsync(exception);
    }

    // O cliente chama este método ao conectar‑se, informando seu nome de usuário.
    public Task Register(string user)
    {
        Console.WriteLine($"[Register] Usuário '{user}' conectado com ConnectionId = {Context.ConnectionId}");
        // Registra ou sobrescreve; se o usuário já estava conectado, a antiga conexão será limpa ao desconectar
        _connections[user] = Context.ConnectionId;
        return Task.CompletedTask;
    }

    // Envia mensagem privada de "fromUser" → "toUser".
    public Task SendMessage(string fromUser, string toUser, string message)
    {
        Console.WriteLine($"[SendMessage] {fromUser} → {toUser}: {message}");
        if (_connections.TryGetValue(toUser, out var connId))
        {
            // entrega apenas ao destinatário
            return Clients.Client(connId).SendAsync("ReceiveMessage", fromUser, message);
        }
        // Se o destinatário não estiver conectado, devolve erro ao remetente
        return Clients.Caller.SendAsync("Error", $"User '{toUser}' is not connected.");
    }
}
