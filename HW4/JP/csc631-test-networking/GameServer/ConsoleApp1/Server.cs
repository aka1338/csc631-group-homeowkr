using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets; 

namespace ConsoleApp1.Properties
{
    class Server
    {
        public static int MaxPlayers { get; private set; }

        public static int Port { get; private set; }

        private static TcpListener tcpListener;

        public void Start(int _maxPlayers, int _port) {
            MaxPlayers = _maxPlayers;
            Port = _port;

            Console.WriteLine("Starting server..."); 

            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Console.WriteLine($"Server started on {Port}.");
        }

        private static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null); 
        }
        
    }
}
