using Microsoft.AspNetCore.SignalR.Client;
using ProjectClient.Class;
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
        Connection connection;

        public Form1()
        {
            InitializeComponent();

            //hidding buttons until connection with server is established
            btnJoin.Enabled = false;
            btnLeave.Enabled = false;

            connection = new Connection();
            connection.SetForm(this);
            
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            txtConnection.Text = "Connecting to server";
            txtConnectionError.Visible = false;

            connection.ConnectToServer();
        }

        public void ReceiveMessage(string userName, string message)
        {
            txtConnection.Text = "User: " + userName + " Message: " + message;
        }

        public void ConnectionTest(string test)
        {
            txtConnection.Text = "Connected succesfuly";
            btnJoin.Enabled = true;
        }

        public void JoinedGroup(string info)
        {
            txtConnectionError.Visible = false;
            txtConnection.Text = info;
            btnJoin.Enabled = false;
            btnLeave.Enabled = true;
        }

        public void RemoveFromGroup(string info)
        {
            txtConnectionError.Visible = false;
            txtConnection.Text = info;
            btnJoin.Enabled = true;
            btnLeave.Enabled = false;
        }

        public void GroupError(string info)
        {
            txtConnectionError.Visible = true;
            txtConnectionError.Text = info;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void btnJoin_Click(object sender, EventArgs e)
        {
            connection.FormJoinGroup();
        }

        private async void btnLeave_Click(object sender, EventArgs e)
        {
            connection.FormLeaveGroup();
        }
    }
}
