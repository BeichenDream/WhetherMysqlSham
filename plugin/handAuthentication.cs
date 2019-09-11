using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WhetherMysqlSham.plugin
{
  public  class handAuthentication : Plugin
    {
        private class ServerInfo
        {
            public int ThreadId { get; set; }
            public string Salt { get; set; }
            private ServerInfo(string salt, int threadId)
            {
                this.ThreadId = threadId;
                this.Salt = salt;
            }
            public static ServerInfo Get(string salt, int threadId)
            {
                return new ServerInfo(salt, threadId);
            }

        }
        private string Host;
        private int Port;
        public bool IsSham()
        {

            ServerInfo one = GetServerInfo();
            ServerInfo two = GetServerInfo();
            if (one == null || two == null)
            {
                return false;
            }
            else if (one.ThreadId == two.ThreadId)
            {
                return true;
            }
            else if (one.Salt == two.Salt)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public void Init(string host, int port)
        {
            this.Host = host;
            this.Port = port;
        }
        private ServerInfo GetServerInfo()
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(this.Host), this.Port); ;
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            int server_Thread_Id = 0;
            string server_Salt = null;
            socket.Connect(iPEndPoint);
            byte[] b_len = new byte[4];
            socket.Receive(b_len);
            b_len[3] = 0x00;
            int len = BitConverter.ToInt32(b_len, 0);
            byte[] server_Greeting = new byte[len];
            socket.Receive(server_Greeting);
            if (server_Greeting[0] == 0xff)
            {
                return null;
            }
            int index = 0;
            while (server_Greeting[index] != 0x00)
            {
                index++;
            }
            index++;
            byte[] b_server_Thread_Id = new byte[4];
            Array.Copy(server_Greeting, index, b_server_Thread_Id, 0, b_server_Thread_Id.Length - 1);
            server_Thread_Id = BitConverter.ToInt32(b_server_Thread_Id, 0);
            index += 4;
            byte[] b_salt1 = new byte[8];
            Array.Copy(server_Greeting, index, b_salt1, 0, b_salt1.Length);
            server_Salt = Encoding.Default.GetString(b_salt1);
            index += 27;
            byte[] b_salt2 = new byte[12];
            Array.Copy(server_Greeting, index, b_salt2, 0, b_salt2.Length);
            server_Salt += Encoding.Default.GetString(b_salt2);
            socket.Dispose();
            return ServerInfo.Get(server_Salt, server_Thread_Id);
        }
    }
}
