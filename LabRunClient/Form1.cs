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
            //Setup of a form 
            InitializeComponent();

            //TODO: remove after testing
            textBox1.Text = "asd";

            //connection to SignalR server hub
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:44398/gamehub")
                .Build();
            //getting answer from server
                connection.On<string, string>("ReceiveMessage", (string userName, string message) =>
                {
                    //TODO: remove after testing
                    Console.WriteLine(userName + " " + message);
                    textBox1.Text = userName + " " + message;
                });
            //trying connect to server 
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
                //connection.StartAsync().Wait();

                //sending to server
                await connection.InvokeCoreAsync("SendMessage", args: new[] { "User1", "Ready" });
                
                
            };
        }
    }
}
