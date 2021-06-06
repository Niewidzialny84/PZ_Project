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
        }
        static public void Disconnect(NetworkStream stream)
        {
            string msg = "{\"msg\":\"Disconnect\"}";
            var byData = Encoding.UTF8.GetBytes(msg);
            var bytes = HeaderParser.Encode(Header.DIS, Convert.ToUInt32(byData.Length));
            stream.Write(bytes, 0, bytes.Length);
            try
            {
                stream.Write(byData, 0, byData.Length);
            }
            catch(System.IO.IOException e){}
        }
        static public Categories GetCategories(NetworkStream stream)
        {      
            string msg = "{\"msg\":\"GimmeList\"}";
            var byData = Encoding.UTF8.GetBytes(msg);
            var bytes = HeaderParser.Encode(Header.ALI, Convert.ToUInt32(byData.Length));
            stream.Write(bytes, 0, bytes.Length);
            try
            {
                stream.Write(byData, 0, byData.Length);
            }
            catch (System.IO.IOException e) { }

            var buffer = new byte[3];
            int messageSize = stream.Read(buffer, 0, 3);
            var head = HeaderParser.Decode(buffer);
            var buffer2 = new byte[head.Item2];
            stream.Read(buffer2, 0, buffer2.Length);
            string receive = Encoding.UTF8.GetString(buffer2);
            //string[] jsonified = new string[9];
            Categories categories = JsonSerializer.Deserialize<Categories>(receive);
            if (head.Item1 == Header.ERR)
            {               
                return categories;
            }
            else
            {

                return categories;
            }

            
        }
        static public PersonalStats GetPersonalStats(NetworkStream stream)
        {
            string msg = "{\"msg\":\"GimmeStats\"}";
            var byData = Encoding.UTF8.GetBytes(msg);
            var bytes = HeaderParser.Encode(Header.STR, Convert.ToUInt32(byData.Length));
            stream.Write(bytes, 0, bytes.Length);
            try
            {
                stream.Write(byData, 0, byData.Length);
            }
            catch (System.IO.IOException e) { }

            var buffer = new byte[3];
            int messageSize = stream.Read(buffer, 0, 3);
            var head = HeaderParser.Decode(buffer);
            var buffer2 = new byte[head.Item2];
            stream.Read(buffer2, 0, buffer2.Length);
            string receive = Encoding.UTF8.GetString(buffer2);
            //string[] jsonified = new string[9];
            PersonalStats personalStats = JsonSerializer.Deserialize<PersonalStats>(receive);
            if (head.Item1 == Header.STA)
            {
                return personalStats;
            }
            else
            {
                return new PersonalStats();
            }

          
        }
        static public Question GetQuestion(NetworkStream stream,string category,int type)
        {
            string msg = "{\"category\":\""+category+"\"}";
            var byData = Encoding.UTF8.GetBytes(msg);
            if(type ==0)
            {
                var bytes = HeaderParser.Encode(Header.QUI, Convert.ToUInt32(byData.Length));
                stream.Write(bytes, 0, bytes.Length);
            }
            else if (type == 1)
            {
                var bytes = HeaderParser.Encode(Header.NXT, Convert.ToUInt32(byData.Length));
                stream.Write(bytes, 0, bytes.Length);
            } 
            try
            {
                stream.Write(byData, 0, byData.Length);
            }
            catch (System.IO.IOException e) { }

            var buffer = new byte[3];
            int messageSize = stream.Read(buffer, 0, 3);
            var head = HeaderParser.Decode(buffer);
            var buffer2 = new byte[head.Item2];
            stream.Read(buffer2, 0, buffer2.Length);
            string receive = Encoding.UTF8.GetString(buffer2);
            //string[] jsonified = new string[9];
            Question question = JsonSerializer.Deserialize<Question>(receive);
            return question;


        }
    }
}
