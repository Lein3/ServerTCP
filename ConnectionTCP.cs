using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerTCP
{
    public static class ConnectionTCP
    {
        public static string ip { get; set; } = "127.0.0.1";
        public static int port { get; set; } = 8080;
        public static IPEndPoint tcpEndPoint { get; set; } = new IPEndPoint(IPAddress.Parse(ip), port);
        public static Socket tcpSocket { get; set; } = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static void Start()
        {
            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(5);
            while(true)
            {
                var listener = tcpSocket.Accept();
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();
                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                } while (listener.Available > 0);

                Console.WriteLine(data);
                listener.Send(Encoding.UTF8.GetBytes("Успех"));
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }
    }
}
