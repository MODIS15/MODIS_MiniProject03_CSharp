using MiniProject3.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniProject3
{
    public class Controller
    {
        
        Node node { get; set; }
        Thread ListenerThread { get; set; }

        Listener listener { get; set; }
        Sender sender { get; set; }

        public Controller(Node.Connection connection, int localPort)
        {
            node = new Node(localPort);
            node.AddConnection(connection);
            listener.controller = this;
        }

        public Controller(int localport)
        {
            node = new Node(localport);
            listener.controller = this;
        }



        public void startListener()
        {
            ListenerThread.Start(new ThreadStart(listener.setUpListener));
        }

        public void joinMessageHandler(JoinMessage message)
        {
            var incomingConnection = new Node.Connection(message.Ip, message.Port);
            //Only store unknown connections
            if (!node.Connections.Contains(incomingConnection))
            {
                node.AddConnection(incomingConnection);
            }
        }



        private void HandlePutMessage(PutMessage message)
        {
            node.AddItem(message.Key, message.Message);
        }

        public void messagehandler(Object message)
        {
            if(message.GetType()==typeof(PutMessage))
            {
                HandlePutMessage((PutMessage) message);
            }
            else if(message.GetType() == typeof(JoinMessage))
            {
                joinMessageHandler((JoinMessage)message);
            }
            else
            {
                Console.WriteLine($"Could not recognize object of type {message.GetType()}");
            }
        }



    }


    

}
