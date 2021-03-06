﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject3.MessageTypes
{
    [Serializable]
    public class JoinMessage : Message
    {
        public string Ip { get; set; }

        public int Port { get; set; }

        protected JoinMessage() { }

        public JoinMessage(string ip, int port) : base(MessageType.Join)
        {
            Ip = ip;
            Port = port;
        }
    }
}
