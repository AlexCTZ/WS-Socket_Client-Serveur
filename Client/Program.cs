using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WS_Serveur
{
    class Program
    {
        private static Socket SeConnecter()
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            int port = 50532;

            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, port);

            client.Connect(iPEndPoint);

            Console.WriteLine("client");

            return client;
        }

        private static void EcouterReseau(Socket clientSocket)
        {
            byte[] receiveBuffer = new byte[1024];
            byte[] sendBuffer;
            int bytesRead;

            while (true)
            {

                // Envoyer des données au serveur (exemple)
                Console.Write("Saisissez votre message : ");
                string message = Console.ReadLine();
                sendBuffer = Encoding.ASCII.GetBytes(message);
                clientSocket.Send(sendBuffer);
                bytesRead = clientSocket.Receive(receiveBuffer);
                if (bytesRead > 0)
                {
                    string dataReceived = Encoding.ASCII.GetString(receiveBuffer, 0, bytesRead);
                    Console.WriteLine($"Serveur dit : {dataReceived}");
                }
            }
        }

        private static void Deconnecter(Socket socket)
        {
            socket.Close();
        }

        static void Main(string[] args)
        {
            Socket SocketClient = SeConnecter();
            EcouterReseau(SocketClient);
            Deconnecter(SocketClient);
        }
    }
}
