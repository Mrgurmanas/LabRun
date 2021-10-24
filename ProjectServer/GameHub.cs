using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServer
{
    public static class GroupHandler
    {
        private const int GROUP_SIZE = 2;
        private static List<string> groupList = new List<string>();

        public static bool CanStartGame()
        {
            return groupList.Count == GROUP_SIZE;
        }

        public static bool CanJoinGroup()
        {
            return groupList.Count < GROUP_SIZE;
        }

        public static List<string> GetGroupList()
        {
            return groupList;
        }

        public static bool Add(string connectionId)
        {
            if (groupList.Count < GROUP_SIZE)
            {
                if (!groupList.Contains(connectionId))
                {
                    groupList.Add(connectionId);
                    return true;
                }
            }
            return false;
        }

        public static bool Remove(string connectionId)
        {
            if (groupList.Contains(connectionId))
            {
                groupList.Remove(connectionId);
                return true;
            }
            return false;
        }
    }

    public class GameHub : Hub
    {

        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public Task ConnectionTest(string test)
        {
            Console.WriteLine("Connection test: " +Context.ConnectionId + " " + test);
            return Clients.Caller.SendAsync("ConnectionTest", test);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            if (GroupHandler.CanJoinGroup())
            {
                if (GroupHandler.Add(Context.ConnectionId)) {
                    await Clients.Group(groupName).SendAsync("JoinedGroup", $"{Context.ConnectionId} has joined the group {groupName}.", Context.ConnectionId);
                    await Clients.Caller.SendAsync("JoinedGroupUpdateId", Context.ConnectionId);
                }
                else
                {
                    await Clients.Caller.SendAsync("GroupError", $"Already joined {groupName} group");
                }
                if (GroupHandler.CanStartGame())
                {
                    //start game
                    await Clients.Group(groupName).SendAsync("StartGroupGameSession", "", GroupHandler.GetGroupList());
                    Console.WriteLine("Started game for Player1 " + GroupHandler.GetGroupList()[0] + " Player2 " + GroupHandler.GetGroupList()[0]);
                    
                }
            }
            else
            {
                // send error
                await Clients.Caller.SendAsync("GroupError", $"Can't join {groupName} group");
            }
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            if (!GroupHandler.Remove(Context.ConnectionId))
            {
                await Clients.Caller.SendAsync("GroupError", $"Can't remove from {groupName} group");
            }
            else
            {
                await Clients.Caller.SendAsync("RemoveFromGroup", $"{Context.ConnectionId} has left the group {groupName}.");
            }
        }

        public Task SpawnCoin(string X, string Y, string groupName)
        {
            int x = int.Parse(X);
            int y = int.Parse(Y);
            return Clients.Group(groupName).SendAsync("SpawnCoin", x, y);
        }

        public Task AddPlayerPoints(string points, string connectionId, string groupName)
        {
            int pointsInt = int.Parse(points);
            return Clients.Group(groupName).SendAsync("AddPlayerPoints", pointsInt, connectionId);
        }

        public Task SpawnSpecialItem(string X, string Y, string ID,  string groupName)
        {
            int x = int.Parse(X);
            int y = int.Parse(Y);
            int id = int.Parse(ID);
            return Clients.Group(groupName).SendAsync("SpawnSpecialItem", x, y, id);
        }
        
        public Task AddPlayerItem(string itemId, string connectionId, string groupName)
        {
            int id = int.Parse(itemId);
            return Clients.Group(groupName).SendAsync("AddPlayerItem", id, connectionId);
        }

        public Task UpdatePlayerPos(string X, string Y, string connectionId, string groupName)
        {
            Console.WriteLine("UpdatePlayerPos X: " + X + " Y: " + Y + " connectionId: " + connectionId + " groupName: " + groupName);
            int x = int.Parse(X);
            int y = int.Parse(Y);
            //await Clients.Group(groupName).SendAsync("UpdatePlayers", x, y, connectionId, groupName);
            return Clients.Group(groupName).SendAsync("UpdatePlayers", x, y, connectionId, groupName);
        }
    }
}
