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
using MiniProject3.Message;

namespace MiniProject3
{
    public class PutClient
    {
        private TcpListener putListener;

        public PutClient(IPAddress address, int port)
        {

        }

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

                // Serialize message
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("PutMessage.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, putMessage);
                stream.Close();

                putListener = new TcpListener(ipEndPoint); // Should maybe start intitially 
                putListener.Start();
                Console.WriteLine("Started listening on put...");
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



    }

}
