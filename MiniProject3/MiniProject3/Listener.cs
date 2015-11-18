using MiniProject3.Message;
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
    class Listener
    {
        TcpClient client { get; set; }
        public Node node{get; set;}
        public Controller controller { get; set; }
        public TcpListener listener { get; private set; }

        public Listener(Node node)
        {
            this.node = node;
            setUpListener();
        }

        public void setUpListener()
        {
            listener = new TcpListener(IPAddress.Any , node.localPort);
            listener.Start();

            while(true)
            {
                client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                IFormatter formatter = new BinaryFormatter();

                var message = formatter.Deserialize(stream);
                client.Close();
            }


            
        }

        
    }
}
