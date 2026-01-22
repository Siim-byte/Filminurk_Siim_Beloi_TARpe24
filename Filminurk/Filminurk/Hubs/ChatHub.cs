using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Web;
namespace Filminurk.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            var userName = Context.User;
            await Clients.All.SendAsync("ReceiveMessage", userName, message);
        }
    }
}