using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject3
{
    public class PutClient
    {
        public PutClient()
        {
            //c
        }

        public void PutRequest(int key, string value)
        {

        }

        public void TcpListener()
        {
            TcpListener server = null;
            try
            {
                int port = 8080;
                IpAdress local
            }
        }


        static void Connect(string ip, int port)
        {
            try
            {
                port = 8080;
                TcpClient client = new TcpClient( ip, port);
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                
            }

        }

    }
}
