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
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Waiting for clients");
                foreach (var i in SocketServers.clients)
                {
                    Console.WriteLine("Conencted client: "+i.ToString());
                    
                }
                
                await Task.Delay(1000);
                if (SocketServers.clients.Count >= 2)
                { break; }

            }

            Console.WriteLine("client connected, Game will start asap");
            await Task.Delay(2000);

            new Gui().Show();

         
        

        }
    }

}