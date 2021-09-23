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

namespace LabRunClient
{
    public partial class Form1 : Form
    {
        HubConnection connection;

        public Form1()
        {
            InitializeComponent();

            textBox1.Text = "asd";

            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:44398/gamehub")//53353/44398/
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
                //connection.StartAsync().Wait();
                await connection.InvokeCoreAsync("SendMessage", args: new[] { "User1", "Ready" });
                connection.On("ReceiveMessage", (string userName, string message) =>
                {
                    Console.WriteLine(userName + " " + message);
                    textBox1.Text = userName + " " + message;
                });
            };
        }
    }
}
