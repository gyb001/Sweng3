using ConsoleApp8;
using ConsoleApp8.Model;
using Server;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sweng3
{
    public class program
    {
        public static void Main()
        {
            run();
            Console.ReadLine();
        }
        public static async Task run()
        {

            SocketServers ret = new SocketServers();
            ret.StartListening();


            //CONNECTING
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Conencted client: " + SocketServers.clients.Count);
                await Task.Delay(1000);
                if (SocketServers.clients.Count >= 2)
                { break; }

            }

            Console.WriteLine("Client connected, Game will start asap");
            await Task.Delay(2000);


            //GUI
            new Gui().Show();

            //START
            bool first = new Random().NextDouble() >= 0.5;
            SocketServers.Send(SocketServers.clients[0], first ? "first" : "secound");
            SocketServers.Send(SocketServers.clients[1], !first ? "first" : "secound");






        }
    }

}