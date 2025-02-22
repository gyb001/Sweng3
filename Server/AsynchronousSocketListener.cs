﻿using Sweng3;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


using System.Linq;
using Server.Model;
using ConsoleApp8.Model;
// State object for reading client data asynchronously  
public class StateObject
{
    // Client  socket.  
    public Socket workSocket = null;
    // Size of receive buffer.  
    public const int BufferSize = 1024;
    // Receive buffer.  
    public byte[] buffer = new byte[BufferSize];
    // Received data string.  
    public StringBuilder sb = new StringBuilder();
}

public class AsynchronousSocketListener
{
    // Thread signal.  
    public ManualResetEvent allDone = new ManualResetEvent(false);

    private static AsynchronousSocketListener instance = null;
    private static readonly object padlock = new object();


    public static AsynchronousSocketListener Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new AsynchronousSocketListener();
                }
                return instance;
            }
        }
    }


    public AsynchronousSocketListener()
    {
    }


    public async Task StartListening()
    {

        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);

       Socket listener = new Socket(IPAddress.Parse("127.0.0.1").AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

       try
        {
            listener.Bind(localEndPoint);
            listener.Listen(100);

            while (true)
            {
                // Set the event to nonsignaled state.  
                allDone.Reset();

                // Start an asynchronous socket to listen for connections.  
             //   Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept(
                    new AsyncCallback(AcceptCallback),
                    listener);

                // Wait until a connection is made before continuing.  
                allDone.WaitOne();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

    

    }

    public void AcceptCallback(IAsyncResult ar)
    {
        // Signal the main thread to continue.  
        allDone.Set();

        // Get the socket that handles the client request.  
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);

        // Create the state object.  
        StateObject state = new StateObject();
        state.workSocket = handler;
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);
    }

    public void ReadCallback(IAsyncResult ar)
    {
        String content = String.Empty;

        // Retrieve the state object and the handler socket  
        // from the asynchronous state object.  
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.workSocket;

        // Read data from the client socket.
        int bytesRead = handler.EndReceive(ar);

        if (bytesRead > 0)
        {
            // There  might be more data, so store the data received so far.  
            state.sb.Append(Encoding.ASCII.GetString(
                state.buffer, 0, bytesRead));

            // Check for end-of-file tag. If it is not there, read
            // more data.  
            content = state.sb.ToString();
            if (content.IndexOf("<EOF>") > -1)
            {

                content = content.Replace("<EOF>","");
                var str = content.Split("|");
                Send(handler, DataHandlerAsync(str[0], str[1], handler));
            }
            else
            {
                // Not all data received. Get more.  
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
            }
        }
    }

    private string DataHandlerAsync(string id, string content, Socket handler)
    {
        if (content == "getState")
        { return  Field.Instance.objectTOjson(); }
        if (content.Contains("setState:"))
        {
            content=content.Replace("setState:", "");
            Field.Instance.jsonTOobject(content); }

        
        Clinet cli;
        cli = program.clients.Where(p => p.id == id).FirstOrDefault();
      //  Console.WriteLine("id: "+id +"\t data: "+ content);
        if (content == "connReq")
        {
            program.clients.Add(new Clinet() { id = id, sck = handler,cmd="" });
            cli = program.clients.Where(p => p.id == id).FirstOrDefault();
        }
        while (cli.cmd == "")
        { Thread.Sleep(500); }
        cli.cmd = "";
        return cli.cmd;
    }

    public void Send(Socket handler, String data)
    {
        // Convert the string data to byte data using ASCII encoding.  
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        // Begin sending the data to the remote device.  
        handler.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), handler);
    }

    private void SendCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the socket from the state object.  
            Socket handler = (Socket)ar.AsyncState;

            // Complete sending the data to the remote device.  
            int bytesSent = handler.EndSend(ar);
            //  Console.WriteLine("Sent {0} bytes to client.", bytesSent);

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }


}