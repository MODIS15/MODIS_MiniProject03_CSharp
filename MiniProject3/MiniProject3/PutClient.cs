using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using MiniProject3.MessageTypes;

namespace MiniProject3
{

    /// <summary>
    /// This program is a Put Client used to send resources (message, key) to the Node class. 
    /// </summary>
    public class PutClient
    {
        // Could optionally pass parameters directly as ip, port, message and key
        public PutClient() 
        {
            while (true) SendResourceMessage();
        }

        /*
        public static void Main(String[] args)
        {
            PutClient putClient = new PutClient();
        }
        */

        /// <summary>
        /// Insert resource into P2P 
        /// </summary>
        private void SendResourceMessage()
        {
            try
            {
                Console.WriteLine("Please enter a valid ip for a given node in the network: ");
                var ip = IPAddress.Parse(Console.ReadLine());

                Console.WriteLine("Please enter the port of the node : ");
                var port = Int32.Parse(Console.ReadLine());

                var ipEndPoint = new IPEndPoint(ip, port);

                Console.WriteLine("Please enter the message to put as a resource in the network: ");
                var resourceInput = Console.ReadLine();

                Console.WriteLine("Please enter the key of the message resource: ");
                var resourceKey = Int32.Parse(Console.ReadLine());

                var putMessage = new PutMessage(resourceKey, resourceInput);
                Console.WriteLine(); // New line 

                // Start TCPClient
                using (var tcpClient = new TcpClient(ipEndPoint))
                {
                    tcpClient.Connect(ipEndPoint);
                    SerializeMessage(tcpClient, putMessage);  
                }

                Console.WriteLine("Message has been put.\n" +
                    "Resetting...\n");
            }
            catch (HostProtectionException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Serializes a given message used to send in the network stream upon requests. 
        /// </summary>
        /// <param name="client">
        /// The TCP client used to transfer Put Messages with. 
        /// </param>
        /// <param name="message">
        /// PutMessage holding a key and a message. 
        /// </param>
        private void SerializeMessage(TcpClient client, PutMessage message)
        {
            IFormatter formatter = new BinaryFormatter();
            NetworkStream stream = client.GetStream();
            formatter.Serialize(stream, message);
            stream.Flush(); 
            stream.Close();
        }

    }

}
