using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DesktopApp
{
    class QuizClient
    {
        protected IPAddress address;
        protected int port;
        protected int Buffer_size = 1024;

        protected char[] trim = { (char)0x0, '\n', '\r' };
        public QuizClient(string address, int port)
        {
            this.address = IPAddress.Parse(address);
            this.port = port;
        }

        public void Start()
        {
            TcpClient client = new TcpClient();
            client.Connect(address, port);
            byte[] buffer = new byte[Buffer_size];
            NetworkStream stream = client.GetStream();
            try
            {
                byte[] byData = Encoding.ASCII.GetBytes("Connect");
                stream.Write(byData, 0, byData.Length);
                int messageSize = stream.Read(buffer, 0, Buffer_size);
                string recive = Encoding.ASCII.GetString(buffer).Trim(trim);
                System.Console.WriteLine(recive);
            }
            catch (Exception e)
            {
                System.Console.Write(e.Message);
            }
        }
    }
}
