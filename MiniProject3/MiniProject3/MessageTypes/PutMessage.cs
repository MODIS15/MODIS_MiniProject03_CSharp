using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject3.MessageTypes
{
    [Serializable]
    public class PutMessage : Message
    {
        public int Key { get; set; }

        public string Message { get; set; }

        protected PutMessage() { }

        public PutMessage(int key, string message) : base(MessageType.Put)
        {
            Key = key;
            Message = message;
        }
    }
}
