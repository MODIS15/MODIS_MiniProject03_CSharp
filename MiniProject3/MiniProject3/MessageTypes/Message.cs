using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject3.MessageTypes
{
    [Serializable]
    public class Message
    {
        public MessageType Type { get; set; }

        public Message() { }

        public Message(MessageType type)
        {
            Type = type;
        }
    }
}
