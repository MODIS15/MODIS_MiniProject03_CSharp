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
            listener.controller(this);
        }

        public Controller(int localport)
        {
            node = new Node(localport);
            listener.controller(this);
        }



        public void startListener()
        {
            ListenerThread.Start(new ThreadStart(Listener.start()));
        }
    }
}
