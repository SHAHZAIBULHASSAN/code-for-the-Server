using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Multipleclients
{
    class Program
    {
        public static object IPE { get; private set; }

        static void Main(string[] args)
        {
            int port = 13000;
            string IpAddress = "127.0.0.1";
            Socket ServerListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IpAddress), port);
            ServerListener.Bind(ep);
            ServerListener.Listen(100);
            Console.WriteLine("server is listening");
            Socket clientsocket = default(Socket);
            int counter = 0;
            Program p = new Program();

            while (true)
            {
                counter++;
                clientsocket = ServerListener.Accept();
                Console.WriteLine("client is connected");
                Thread userthread = new Thread(new ThreadStart(()=>p.User(clientsocket)));
                userthread.Start();
            }
        }


        public void User(Socket client)
        {

            while (true)
            {
                //byte[] msg = new byte[1024];
                //int size = client.Receive(msg);
                string s = "Hello froom Server";
                byte[] msg = Encoding.UTF8.GetBytes(s);

                client.Send(msg, 0, msg.Length, SocketFlags.None);


            }
        }
    }
}
