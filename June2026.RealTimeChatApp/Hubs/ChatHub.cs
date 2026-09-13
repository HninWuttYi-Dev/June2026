using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace June2026.RealTimeChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task ServiceReceiveMessageEvent(string user, string message)
        {
            await Clients.All.SendAsync("ClientReceiveMessageEvent", user, message);
        }
    }
}