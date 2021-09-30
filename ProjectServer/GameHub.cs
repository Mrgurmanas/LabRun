using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServer
{
    public class GameHub : Hub
    {
        //private const int GROUP_SIZE = 2;
        //private List<string> groupList = new List<string>();

        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public Task ConnectionTest(string test)
        {
            return Clients.Caller.SendAsync("ConnectionTest", test);
        }

        public async Task AddToGroup(string groupName)
        {
                    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

                    await Clients.Group(groupName).SendAsync("JoinedGroup", $"{Context.ConnectionId} has joined the group {groupName}.", Context.ConnectionId);
                    await Clients.Caller.SendAsync("MemberConnection", Context.ConnectionId);
        }

        public async Task StartGroupGameSession(string groupName)
        {
            await Clients.Group(groupName).SendAsync("StartGroupGameSession", "");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("RemoveFromGroup", $"{Context.ConnectionId} has left the group {groupName}.", Context.ConnectionId);
        }
    }
}
