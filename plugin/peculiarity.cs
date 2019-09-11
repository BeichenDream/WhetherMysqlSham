using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WhetherMysqlSham.plugin
{
  public  class peculiarity : Plugin
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
            for (int i = 0; i < 15; i++)
            {
                int code = GetErrorCode();
                if (code == 1129)
                {
                    return false;
                }
            }
            return true;
        }
        public int GetErrorCode()
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(this.Host), this.Port); ;
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            byte[] data = new byte[7];
            socket.Connect(iPEndPoint);
            socket.Receive(data);
            byte[] code = new byte[4];
            Array.Copy(data, 5, code, 0, 2);
            socket.Dispose();
            return BitConverter.ToInt32(code, 0);
        }
    }
}
