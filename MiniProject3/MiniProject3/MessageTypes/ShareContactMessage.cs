using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject3.MessageTypes
{
    [Serializable]
    public class ShareContactMessage : Message
    {
        Node.Connection Contact { get; set; }

        protected ShareContactMessage() { }

        public ShareContactMessage(Node.Connection Contact) : base(MessageType.ShareContact)
        {
            this.Contact = Contact;
        }
    }
}
