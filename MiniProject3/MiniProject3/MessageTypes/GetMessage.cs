using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject3.MessageTypes
{
    [Serializable]
    public class GetMessage : Message
    {
        public int Key { get; set; }

        public string Ip { get; set; }

        public int Port { get; set; }

        protected GetMessage() { }

        public GetMessage(int key, string ip, int port) : base(MessageType.Get)
        {
            Key = key;
            Ip = ip;
            Port = port;
        }
    }
}
