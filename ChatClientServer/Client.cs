using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatClient
{

    class Client
    {
        static void Main(string[] args)
        {
            RunClient();
        }

        static void RunClient()
        {
            Console.WriteLine("CLIENT MACHINE !");

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = 8080;
            string serverAddress;
            Console.Write("Input Server IP Address:");
            serverAddress = Console.ReadLine();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(serverAddress), port);

            try
            {
                socket.Connect(endPoint);
                Console.WriteLine($"Connected to Server at port:{port}");

                while(true)
                {
                    string input;
                    Console.Write("Client : ");
                    input = Console.ReadLine();

                    byte[] sentMessage = Encoding.ASCII.GetBytes(input);
                    int sentMessageLength = socket.Send(sentMessage);

                    if (input.IndexOf("exit") > -1)
                        break;

                    byte[] receivedMessage = new byte[1024];
                    int receivedMessageLength = socket.Receive(receivedMessage);

                    string data;
                    data = Encoding.ASCII.GetString(receivedMessage, 0, receivedMessageLength);
                    Console.Write("Server : ");
                    Console.WriteLine(data);

                    if (data.IndexOf("exit") > -1 )
                        break;
                }

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch
            {
                Console.WriteLine("Error: Unable to connect to Server!");
            }
        }
    }
}