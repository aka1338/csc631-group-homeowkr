using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace GameServer
{
    class Client
    {
        public static int dataBufferSize = 4096; 

        public int id;
        public TCP tcp;

        public class TCP 
        {
            public TcpClient socket;

            private readonly int id;
            private NetworkStream stream;
            private byte[] recieveBuffer; 

            public TCP(int _id) 
            {
                id = _id; 
            }

            public void Connect(TcpClient _socket) 
            {
                socket = _socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();
                 
                recieveBuffer = new byte[dataBufferSize];
                stream.BeginRead(recieveBuffer, 0, dataBufferSize, ReceiveCallback, null); 

                // TODO: send welcome packet 
            
            }

            private void RecieveCallback(IAsyncResult _result)
            {
                try
                {
                    int _byteLength = stream.EndRead(_result);
                    if (_byteLength <= 0) 
                    {
                        //TODO: disconnect
                        return; 
                    }

                    byte[] _data = new byte[_byteLength];
                    Array.Copy(recieveBuffer, _data, _byteLength); 

                    // TODO: handle data 

                }

                catch (Exception _ex) 
                {
                    Console.WriteLine($"Error receiving TCP data: {_ex}"); 

                }

            }
        
        }
    }
}
