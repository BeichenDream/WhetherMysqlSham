using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WhetherMysqlSham.plugin
{
 public   class handCcontrast : Plugin
    {
        private string Host;
        private int Port;
        public void Init(string host, int port)
        {
            this.Host = host;
            this.Port = port;
        }

        public bool IsSham()
        {
                byte[] one = GetServerGreeting();
                byte[] two = GetServerGreeting();
                if (Enumerable.SequenceEqual(one, two) && one[0] != 0xff && two[0] != 0xff&&one.Length>0&&two.Length>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

        }
        public byte[] GetServerGreeting()
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(this.Host), this.Port); ;
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(iPEndPoint);
            byte[] b_len = new byte[4];
            socket.Receive(b_len);
            b_len[3] = 0x00;
            int len = BitConverter.ToInt32(b_len, 0);
            byte[] server_Greeting = new byte[len];
            socket.Receive(server_Greeting);
            socket.Dispose();
            return server_Greeting;
        }
    }
}
