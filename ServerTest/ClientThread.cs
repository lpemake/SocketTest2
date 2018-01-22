using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;
namespace ServerTest
{
    // tässä luokassa on asiakkaan palveleminen
    // omassa säikeessään (sivu 34)
    class ClientThread
    {
        private TcpClient client;
        public ClientThread(TcpClient client)
        {
            this.client = client;
        }

        // ajetaan omassa säikeessään
        // Muokkaa sivulla 34 olevan mallin mukaan
        public void ServeClient()
        {
            // avataan yhteydet
            // avataan streamit
            SocketUtilities.SocketUtilityClass su =
                new SocketUtilities.SocketUtilityClass(client);

            su.Open();

            bool jatka = true;
            while (jatka)
            {
                // Muuta ohjelmaa siten, että säie loppuu, jos asiakkaasta ei ole kuulunut
                // 60 sekuntiin mitään
                
                DateTime viimeinenKomento = DateTime.Now;

                if (su.DataAvailable())
                {

                    // luetaan ja käsitellään komennot

                    string komento = su.ReadMessage();
                    viimeinenKomento = DateTime.Now;

                    string vastaus = "";
                    // päivitetään aika
                    switch (komento)
                    {
                        case SocketUtilities.Commands.TIME:
                            vastaus = DateTime.Now.ToString();
                            break;
                        case SocketUtilities.Commands.NUMBER_OF_CLIENTS:
                            vastaus = "1"; // TODO
                            break;
                        case SocketUtilities.Commands.QUIT:
                            vastaus = "lopetus";
                            jatka = false;
                            break;
                    }
                    su.WriteMessage(vastaus);
                    Console.WriteLine(vastaus);
                }
                else // if (ns.DataAvailable)
                {
                    // lopetetaan, jos viimeisestä komennosta on kulunut yli minuutti
                    DateTime nyt = DateTime.Now;

                    TimeSpan erotus = nyt - viimeinenKomento;
                    if (erotus.TotalSeconds > 60)
                        jatka = false;
                    else
                        Thread.Sleep(500);
                }
            }
            // suljetaan yhteydet
            su.Close();
        }
    }
}
