using WhetherMysqlSham.moudel.ServerGreeting;

namespace WhetherMysqlSham.moudel
{
    class ServerInfo
    {
        public int Protocol { get; set; }
        public string Version { get; set; }
        public int ThreadId { get; set; }
        public string Salt { get; set; }
        public server_language Language { get; set; }
        public ServerCapabilities ServerCapabilities { get; set; }
        public ServerStatus ServerStatus { get; set; }
        public ExtendedServerCapabilities ExtendedServerCapabilities { get; set; }
        public string Authentication_Plugin { get; set; }
    }
}
