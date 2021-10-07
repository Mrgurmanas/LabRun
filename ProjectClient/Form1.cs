using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectClient
{
    public partial class Form1 : Form
    {
        HubConnection connection;
        private const string GROUP_NAME = "Game Hub";
        private const int GROUP_SIZE = 2;
        private String connectionId = "";
        private GameMap map;

        public Form1()
        {
            InitializeComponent();

            //hidding buttons until connection with server is established
            btnJoin.Enabled = false;
            btnLeave.Enabled = false;
            
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            txtConnection.Text = "Connecting to server";
            txtConnectionError.Visible = false;

            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/gamehub")
                .Build();

            ServerResponseHandling();

            connection.StartAsync();
            connection.InvokeCoreAsync("ConnectionTest", args: new[] { "testing connection"});

            //trying connect to server 
            //TODO: need to fix
            connection.Closed += async (error) =>
            {
                txtConnection.Text = "Trying to connect to server";
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();

                //sending to server
                //await connection.SendAsync("SendMessage", new[] { "User1", "Ready" });
                await connection.InvokeCoreAsync("SendMessage", args: new[] { "User1", "Ready" });
            };
        }

        private void ServerResponseHandling()
        {
            connection.On("UpdatePlayers", (int X, int Y, string connectionId, string groupName) =>
            {
                connection.InvokeCoreAsync("ConnectionTest", args: new[] { "conenction UpdatePlayers" });
                map.UpdatePlayerByServer(X, Y, connectionId);
                
            });

            connection.On("ReceiveMessage", (string userName, string message) =>
            {
                txtConnection.Text = "User: " + userName + " Message: " + message;
            });

            connection.On("ConnectionTest", (string test) =>
            {
                txtConnection.Text = "Connected succesfuly";
                btnJoin.Enabled = true;
            });

            connection.On("JoinedGroupUpdateId", (string ConnectionId) =>
            {
                connectionId = ConnectionId;
            });

            connection.On("JoinedGroup", (string info, string ConnectionId) =>
            {
                txtConnectionError.Visible = false;
                txtConnection.Text = info;
                btnJoin.Enabled = false;
                btnLeave.Enabled = true;
            });
            
                connection.On("RemoveFromGroup", (string info) =>
                {
                    txtConnectionError.Visible = false;
                    txtConnection.Text = info;
                    btnJoin.Enabled = true;
                    btnLeave.Enabled = false;
                   

                    connectionId = "";
                });

            connection.On("StartGroupGameSession", (string info, List<string> groupMemebers) =>
            {
                map = new GameMap(groupMemebers, GROUP_NAME, connectionId, connection);
                map.ShowDialog();
            });

            connection.On("GroupError", (string info) =>
            {
                txtConnectionError.Visible = true;
                txtConnectionError.Text = info;

            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void btnJoin_Click(object sender, EventArgs e)
        {
            await connection.InvokeCoreAsync("JoinGroup", args: new[] { GROUP_NAME });
        }

        private async void btnLeave_Click(object sender, EventArgs e)
        {
            await connection.InvokeCoreAsync("RemoveFromGroup", args: new[] { GROUP_NAME });
        }
    }
}
