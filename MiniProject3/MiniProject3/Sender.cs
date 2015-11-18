using MiniProject3.MessageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject3
{
    public class Sender
    {
        
        public void shareContact(Node.Connection contact )
        {
            IPEndPoint ipEndPoint = new IPEndPoint(contact.IpAddress, contact.Port);
          
            var ShareContactMessage = new ShareContactMessage(contact);
            using (var tcpClient = new TcpClient(ipEndPoint))
            {
                tcpClient.Connect(ipEndPoint);
                SerializeMessage(tcpClient, ShareContactMessage);
                
            }
        }

        private void SerializeMessage(TcpClient client, ShareContactMessage message)
        {
            IFormatter formatter = new BinaryFormatter();
            NetworkStream stream = client.GetStream();
            formatter.Serialize(stream, message);
            stream.Flush();
            stream.Close();
        }
    }
}
