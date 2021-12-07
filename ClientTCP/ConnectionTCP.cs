using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientTCP
{
    public static class ConnectionTCP
    {
        public static string ip { get; set; } = "127.0.0.1";
        public static int port { get; set; } = 8080;
        public static IPEndPoint tcpEndPoint { get; set; } = new IPEndPoint(IPAddress.Parse(ip), port);
        public static Socket tcpSocket { get; set; } = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static void Send()
        {
            Console.WriteLine("Введите сообщение");
            var message = Console.ReadLine();
            var data = Encoding.UTF8.GetBytes(message);
            tcpSocket.Connect(tcpEndPoint);
            tcpSocket.Send(data);

            var buffer = new byte[256];
            var size = 0;
            var answer = new StringBuilder();
            do
            {
                size = tcpSocket.Receive(buffer);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            } while (tcpSocket.Available > 0);

            Console.WriteLine(answer);
            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();
        }
    }
}
