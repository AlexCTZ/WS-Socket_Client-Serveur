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
            Socket serveur = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            int port = 50532;

            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, port);

            serveur.Bind(iPEndPoint);
            serveur.Listen(10);

            Console.WriteLine("Serveur");

            return serveur;
        }

        private static Socket AccepterConnexion(Socket serveurSocket)
        {
            Socket client = serveurSocket.Accept();

            IPEndPoint clientEndpoint = (IPEndPoint)client.RemoteEndPoint;

            Console.WriteLine($"adresse ip : {clientEndpoint.Address} port : {clientEndpoint.Port}");

            return client;
        }

        private static void EcouterReseau(Socket clientSocket)
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            while (true)
            {
                // Recevoir des données du client
                bytesRead = clientSocket.Receive(buffer);
                if (bytesRead > 0)
                {
                    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Client dit : {dataReceived}");

                    // Envoyer des données au client (exemple)
                    string response = Console.ReadLine();
                    byte[] responseData = Encoding.ASCII.GetBytes(response);
                    clientSocket.Send(responseData);
                }
            }
        }

        private static void Deconnecter(Socket socket)
        {
            socket.Close();
        }

        static void Main(string[] args)
        {
            Socket SocketServer = SeConnecter();
            Socket SocketClient = AccepterConnexion(SocketServer);
            EcouterReseau(SocketClient);
            Deconnecter(SocketServer);
        }
    }
}