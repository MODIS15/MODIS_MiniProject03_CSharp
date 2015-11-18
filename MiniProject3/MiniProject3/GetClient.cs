using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using MiniProject3.MessageTypes;

namespace MiniProject3
{
    /// <summary>
    /// The GetClient is used to retrieve resources (message, key) from a Node.
    /// The GetClient process takes the IP/port of a Node and an integer key as arguments. 
    /// The GetClient then submits a GET(key, ip2, port2) message to the indicated Node.
    /// The GetClient then listens on ip2/port2 for a PUT(key, value) message.
    /// If the PUT message arrives, the Node network has stored the association (key, value), thus some PutClient previously issued that PUT message. 
    /// </summary>
    public class GetClient
    {

        private TcpListener incomingMessagesListener;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="port">
        /// The port to the Node from which a resource is retrieved 
        /// </param>
        public GetClient(int port) 
        {
            // Accept connections from Any ip on specified ports 
            incomingMessagesListener = new TcpListener(IPAddress.Any, port);
        }

        /// <summary>
        /// Initializes a TcpListener used to listen for incoming resources. 
        /// The listener starts from the main thread as soon as the input is received from the console. 
        /// Listen tasks are delegated and set to run in separate threads from main thread.
        /// Tasks are used 
        /// </summary>
        private void Start()
        {
            incomingMessagesListener.Start();;
            Console.WriteLine("Listening...");
            new Task(() => Listen(incomingMessagesListener)).Start(); // Run Listen tasks in separate threads 
        }

        /// <summary>
        /// Sends a Get message to a given node holding the specified resource from user input.
        /// </summary>
        /// <param name="key">
        /// Key used to retrieve the resource. 
        /// </param>
        /// /// <param name="ipAddress">
        /// Ip address of Node from which resource is retrieved. 
        /// </param>
        /// /// <param name="port">
        /// Port of Node from which resource is retrieved.  
        /// </param>
        private void SendResourceRequest(int key, string ipAddress, int port)
        {
            var getMessage = new GetMessage(key, ipAddress, port);
            var byteMessage = ObjectToBytes(getMessage);
            var host = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            using (var client = new TcpClient(ipAddress, port))
            {
                client.Connect(host);
                var stream = client.GetStream();
                try
                {
                    stream.Write(byteMessage, 0, byteMessage.Length);
                }
                catch (IOException)
                {
                    Console.WriteLine("There was a failure while writing to the network");
                    Console.WriteLine("Closing application");
                    // return;
                }
            }

        }

        /// <summary>
        /// Listens for incoming resources using a TCPListener. 
        /// </summary>
        private void Listen(TcpListener listener)
        {
            while (true)
            {
                var receivedMessage = DeserializeIncomingMessage(listener);
                Console.WriteLine(receivedMessage.Message); // Print out received message content 
            }
        }


        /// <summary>
        /// This method is used to deserialize the content of a PutMessage retrieved while listening.
        /// The method only works for inputs of type PutMessage.
        /// </summary>
        /// <param name="listener">
        /// The TCP Listner used to track incoming resources from Nodes. 
        /// </param>
        /// <returns></returns>
        private PutMessage DeserializeIncomingMessage(TcpListener listener)
        {
            var client = listener.AcceptTcpClient();
            var stream = client.GetStream();
            try
            {
                IFormatter formatter = new BinaryFormatter();
                var message = formatter.Deserialize(stream);

                if (message.GetType() == typeof (PutMessage))
                {
                    return (PutMessage) message;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                stream.Close();
            }
            throw new ArgumentException("This is not a Put Message");
        }

        /// <summary>
        /// Converts a message object to its underlying array of bytes.
        /// </summary>
        /// <param name="message">
        /// Object representing Get message.
        /// </param>
        /// <returns>
        /// A byte representation of a message. 
        /// </returns>
        private static byte[] ObjectToBytes(Object message)
        {
            if (message == null) return null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, message);
                return memoryStream.ToArray();
            }
        }

        /*
        public static void Main(string[] args)
        {
            {
                try
                {
                    Console.WriteLine("Please write the port used to listen on in the GetClient: ");
                    int port = Int32.Parse(Console.ReadLine());
                    var getClient = new GetClient(port);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        */

    }
}
