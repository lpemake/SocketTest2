using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 8221;
            TcpClient client = new TcpClient("localhost", port);
            SocketUtilities.SocketUtilityClass su =
                new SocketUtilities.SocketUtilityClass(client);

            su.Open();            

            string vastaus = "";
            while (true)
            {
                Console.WriteLine("Anna komento");
                string komento = Console.ReadLine();

                // lähetetään komento
                su.WriteMessage(komento);

                // odotetaan vastausta
                vastaus = su.ReadMessage();
                Console.WriteLine(vastaus);
                if (komento == SocketUtilities.Commands.QUIT)
                    break;
            }

            su.Close();

            Console.ReadKey();
        }
    }
}
