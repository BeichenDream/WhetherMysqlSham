using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WhetherMysqlSham.moudel;
using WhetherMysqlSham.moudel.ServerGreeting;
namespace WhetherMysqlSham.plugin.NoReflection
{
    class GetServerInfo
    {
        private string Host { get; set; }
        private int Port { get; set; }

        public void Init(string host, int port)
        {
            this.Host = host;
            this.Port = port;
        }

        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();
            ServerInfo serverInfo = Get_ServerInfo();
            if (serverInfo==null)
            {
                return "NULL";
            }
            
            stringBuilder.Append(string.Format("Protocol：{0}\r\n", serverInfo.Protocol));
            stringBuilder.Append(string.Format("Version：{0}\r\n", serverInfo.Version));
            stringBuilder.Append(string.Format("ThreadId：{0}\r\n", serverInfo.ThreadId));
            stringBuilder.Append(string.Format("Language：{0}\r\n", Enum.GetName(serverInfo.Language.GetType(),serverInfo.Language)));
            stringBuilder.Append(string.Format("Salt：{0}\r\n", serverInfo.Salt));
            stringBuilder.Append(string.Format("Authentication_Plugin：{0}\r\n", serverInfo.Authentication_Plugin));
            stringBuilder.Append(GetServerGreetingStr(serverInfo.ServerCapabilities.GetType(),serverInfo.ServerCapabilities));
            stringBuilder.Append(GetServerGreetingStr(serverInfo.ServerStatus.GetType(),serverInfo.ServerStatus));
            stringBuilder.Append(GetServerGreetingStr(serverInfo.ExtendedServerCapabilities.GetType(),serverInfo.ExtendedServerCapabilities));
            return stringBuilder.ToString();
        }
        public string GetServerGreetingStr(Type type,object o) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("TypeName:{0}\r\n",type.Name));
            foreach (var item in type.GetMembers())
            {
                foreach (var item2 in item.GetCustomAttributes())
                {
                    Annotation annotation = item2 as Annotation;
                    if (annotation!=null)
                    {
                        stringBuilder.Append(string.Format("\t{0}:{1}\r\n",annotation.Explain,(Sate)type.GetProperty(item.Name).GetValue(o,null)));
                    }

                }
            }
            return stringBuilder.ToString();

        }
        public ServerInfo Get_ServerInfo() {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(this.Host), this.Port); ;
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(iPEndPoint);
            byte[] b_len = new byte[4];
            socket.Receive(b_len);
            b_len[3] = 0x00;
            int len = BitConverter.ToInt32(b_len, 0);
            byte[] server_Greeting = new byte[len];
            socket.Receive(server_Greeting);
            if (server_Greeting[0] == 0xff||server_Greeting.Length<0)
            {
                return null;
            }
            ServerInfo sv = new ServerInfo();
            int index = 0;
            byte[] Protocol = new byte[4];
            Protocol[0] = server_Greeting[index];
            index++;
            sv.Protocol = BitConverter.ToInt32(Protocol, 0);
            while (server_Greeting[index] != 0x00)
            {
                index++;
            }
            byte[] Version = new byte[index - 1];   //版本后面的0X00 是分隔符要去掉
            Array.Copy(server_Greeting, 1, Version, 0, Version.Length); //从下标1开始 下标0是协议版本
            sv.Version = Encoding.Default.GetString(Version);
            index++;
            byte[] ThreadId = new byte[4];
            Array.Copy(server_Greeting, index, ThreadId, 0, 4);
            sv.ThreadId = BitConverter.ToInt32(ThreadId, 0);
            index += 4;
            byte[] Salt = new byte[20];
            Array.Copy(server_Greeting, index, Salt, 0, 8);//Salt 8位
            index += 9;//0x00 分隔符 去掉
            byte[] ServerCapabilities = new byte[2];
            Array.Copy(server_Greeting, index, ServerCapabilities, 0, 2);
            sv.ServerCapabilities = new ServerCapabilities(ServerCapabilities);
            index += 2;
            byte[] ServerLanguage = new byte[4];
            ServerLanguage[0] = server_Greeting[index];
            sv.Language = (server_language)BitConverter.ToInt32(ServerLanguage, 0);
            index++;
            byte[] ServerStatus = new byte[2];
            Array.Copy(server_Greeting, index, ServerStatus, 0, 2);
            sv.ServerStatus = new ServerStatus(ServerStatus);
            index += 2;
            byte[] ExtendedServerCapabilities = new byte[2];
            Array.Copy(server_Greeting, index, ExtendedServerCapabilities, 0, 2);
            sv.ExtendedServerCapabilities = new ExtendedServerCapabilities(ExtendedServerCapabilities);
            index += 2;
            byte[] AuthenticationPluginLength = new byte[4];
            AuthenticationPluginLength[0] = server_Greeting[index];
            index += 11;//插件长度后 10个无用字符
            Array.Copy(server_Greeting, index, Salt, 8, 12);
            sv.Salt = Encoding.Default.GetString(Salt);
            index += 13;//0x00 分隔符           
            byte[] AuthenticationPlugin = new byte[BitConverter.ToInt32(AuthenticationPluginLength, 0)];
            Array.Copy(server_Greeting, index, AuthenticationPlugin, 0, AuthenticationPlugin.Length);
            sv.Authentication_Plugin = Encoding.Default.GetString(AuthenticationPlugin);
            socket.Dispose();
            return sv;
        }
    }
}
