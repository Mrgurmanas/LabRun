using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClient.Class
{
    public class Connection
    {
        HubConnection connection;
        private const string GROUP_NAME = "Game Hub";
        private String connectionId = "";

        Form1 form;
        GameMap gameMap;

        public Connection()
        {

        }

        public void SetForm(Form1 form)
        {
            this.form = form;
        }

        public void SetGameMap(GameMap gameMap)
        {
            this.gameMap = gameMap;
        }

        public void GameMapUpdatePlayerPos(string playerX, string playerY, string connectionId, string groupName)
        {
            connection.InvokeCoreAsync("UpdatePlayerPos", args: new[] { playerX, playerY, connectionId, groupName });
        }

        public void FormJoinGroup()
        {
            connection.InvokeCoreAsync("JoinGroup", args: new[] { GROUP_NAME });
            //await connection.InvokeCoreAsync("JoinGroup", args: new[] { GROUP_NAME });
        }

        public void FormLeaveGroup()
        {
            connection.InvokeCoreAsync("RemoveFromGroup", args: new[] { GROUP_NAME });
            //await connection.InvokeCoreAsync("RemoveFromGroup", args: new[] { GROUP_NAME });
        }

        public void ConnectToServer()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/gamehub")
                .Build();

            ServerResponseHandling();

            connection.StartAsync();
            connection.InvokeCoreAsync("ConnectionTest", args: new[] { "testing connection" });

            //trying connect to server 
            //TODO: need to fix
            /*connection.Closed += async (error) =>
            {
                //txtConnection.Text = "Trying to connect to server";
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();

                //sending to server
                //await connection.SendAsync("SendMessage", new[] { "User1", "Ready" });
                await connection.InvokeCoreAsync("SendMessage", args: new[] { "User1", "Ready" });
            };*/
        }

        private void ServerResponseHandling()
        {
            connection.On("UpdatePlayers", (int X, int Y, string connectionId, string groupName) =>
            {
                //remove 
                connection.InvokeCoreAsync("ConnectionTest", args: new[] { "conenction UpdatePlayers" });
                //connection.InvokeCoreAsync("ConnectionTest", args: new[] { "conenction UpdatePlayers" });
                gameMap.UpdatePlayerByServer(X, Y, connectionId);
            });

            connection.On("ReceiveMessage", (string userName, string message) =>
            {
                form.ReceiveMessage(userName, message);
            });

            connection.On("ConnectionTest", (string test) =>
            {
                form.ConnectionTest(test);
            });

            connection.On("JoinedGroupUpdateId", (string ConnectionId) =>
            {
                connectionId = ConnectionId;
            });

            connection.On("JoinedGroup", (string info, string ConnectionId) =>
            {
                form.JoinedGroup(info);
            });

            connection.On("RemoveFromGroup", (string info) =>
            {
                form.RemoveFromGroup(info);
                connectionId = "";
            });

            connection.On("StartGroupGameSession", (string info, List<string> groupMemebers) =>
            {
               gameMap = new GameMap(groupMemebers, GROUP_NAME, connectionId, this);
                gameMap.Show();
               //gameMap.ShowDialog();

               //remove 
               connection.InvokeCoreAsync("ConnectionTest", args: new[] { "conenction UpdatePlayers" });
            });

            connection.On("GroupError", (string info) =>
            {
                form.GroupError(info);
            });
        }
    }
}
