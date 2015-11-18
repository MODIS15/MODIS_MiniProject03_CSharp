using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject3
{
    public class Node
    {
        public int localPort { get; protected set; }
        public List<Connection> Connections { get; private set; }
        public Dictionary<int, string> StoredFiles { get; private set; }

        
        public Node(int localPort)
        {
            this.localPort = localPort;
        }

        public void AddConnection(Connection connection)
        {
            Connections.Add(connection);
        }

        public void RemoveConnection(Connection connection)
        {
            Connections.Remove(connection);
        }

        public void AddItem(int GUID, string item)
        {
            StoredFiles.Add(GUID, item);
        }

        public void RemoveItem(int GUID, string item)
        {
            StoredFiles.Remove(GUID);
        }

        public class Connection
        {
            public string IpAddress { get; protected set; }
            public int Port { get; protected set; }

            public Connection(string IpAddress, int Port)
            {
                this.IpAddress = IpAddress;
                this.Port = Port;
            }

        }
    }
}
