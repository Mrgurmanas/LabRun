using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ProjectClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/gamehub")
                .Build();

            ServerResponseHandling(connection);

            connection.StartAsync();
            connection.InvokeCoreAsync("ConnectionTest", args: new[] { "testing connection" });

            connection.Closed += async (error) =>
            {
                Console.WriteLine("Closed");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
                await connection.InvokeCoreAsync("SendMessage", args: new[] { "user1", "message" });
            };
 
            Console.ReadKey();
        }

        private static void ServerResponseHandling(HubConnection connection)
        {
            connection.On("ReceiveMessage", (string userName, string message) =>
            {
                Console.WriteLine("User: " + userName + " Message: " + message);
            });

            connection.On("ConnectionTest", (string test) =>
            {
                Console.WriteLine("Connected succesfuly");
            });

            connection.On("JoinedGroup", (string info) =>
            {
                Console.WriteLine(info);
            });

            connection.On("GroupError", (string info) =>
            {
                Console.WriteLine(info);
            });
        }
    }
}
