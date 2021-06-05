using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Protocol;
using System.Text.Json;



namespace DesktopApp
{
    class QuizClient
    {
        protected IPAddress address;
        protected int port;
        protected int Buffer_size = 1024;
        public string username;

        protected char[] trim = { (char)0x0, '\n', '\r' };
        public QuizClient(string address, int port)
        {
            this.address = IPAddress.Parse(address);
            this.port = port;
        }

        public NetworkStream Start()
        {
            TcpClient client = new TcpClient();
            client.Connect(address, port);
           
            NetworkStream stream = client.GetStream();
            return stream;
           
        }
        public bool Login(string passwd, NetworkStream stream)
        {
            //(0b0000_0011)
            User user = new User(username, passwd);
            var jsonfied = JsonSerializer.Serialize(user);
            var byData = Encoding.UTF8.GetBytes(jsonfied);

            var bytes = HeaderParser.Encode(Header.LOG,Convert.ToUInt32(byData.Length));

            stream.Write(bytes, 0, bytes.Length);
            stream.Write(byData, 0, byData.Length);

            var buffer = new byte[3];
            int messageSize = stream.Read(buffer, 0, 3);
            var head = HeaderParser.Decode(buffer);
            var buffer2 = new byte[head.Item2];
            stream.Read(buffer2, 0, buffer2.Length);
            string receive = Encoding.ASCII.GetString(buffer2);

            if(head.Item1==Header.SES)
            {
                return true;
            }
            else 
            {
                return false;
            }
            


                                            

            //string receive = Encoding.ASCII.GetString(buffer).Trim(trim);
           

        }
    }
}
