using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject3
{
    class Listener
    {
        public Node node{get; set;}
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
            TcpClient client;

            while(true)
            {
                client = listener.AcceptTcpClient();
                client.r
                
            }
        }
    }
}
