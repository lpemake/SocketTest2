using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace ServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Parse("127.0.0.1");
            TcpListener tcpListener = new TcpListener(address, 8221);
            tcpListener.Start();

            bool ok = true;
            while(ok)
            {
                // odotetaan uutta yhteyspyyntöä
                TcpClient client = tcpListener.AcceptTcpClient();

                ClientThread clientThread = new ClientThread(client);
                Thread thread = new Thread(clientThread.ServeClient);

                thread.Start();

            }

        }
    }
}
