using ConsoleApp8.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{

    public class SocketServers
    {



       public static List<Socket> clients = new List<Socket>();


        private Socket listener;
        private byte[] buffer = new byte[8192];

        public async Task StartListening()
        {
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.Any, 9999));
            listener.Listen(20);
            listener.BeginAccept(OnSocketAccepted, null);
        }

        private void OnSocketAccepted(IAsyncResult result)
        {
            Socket client = listener.EndAccept(result);

            client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnDataReceived, client);
            listener.BeginAccept(OnSocketAccepted, null);
        }

        private void OnDataReceived(IAsyncResult result)
        {
            Socket client = result.AsyncState as Socket;
            int received = client.EndReceive(result);

            string ret = Encoding.ASCII.GetString(buffer, 0, buffer.Length);


            if (ret.Contains("connection request"))
            {
              //  IPEndPoint remoteIpEndPoint = client.RemoteEndPoint as IPEndPoint;
                clients.Add(client);//remoteIpEndPoint.Address);
                Send(client, "connected");
                Send(client, "waiting");
            }
            else
                Field.Instance.GetCellsFronJson(ret);

            buffer = new byte[8192];
            client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnDataReceived, client);
        }

        public static void Send(Socket client, String data)
        {
          byte[] byteData = Encoding.ASCII.GetBytes(data);

            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }

}
