using System;
using System.Reflection;
using System.Linq;
namespace WhetherMysqlSham.moudel.ServerGreeting
{
    class ServerCapabilities
    {
        [Annotation(0, "new more secure passwords")]
        public Sate LONG_PASSWORD { get; set; }
        [Annotation(1, "Found instead of affected rows")]
        public Sate FOUND_ROWS { get; set; }
        [Annotation(2, "Get all column flags")]
        public Sate LONG_FLAG { get; set; }
        [Annotation(3, "One can specify db on connect")]
        public Sate CONNECT_WITH_DB { get; set; }
        [Annotation(4, "Do not allow database.table.column")]
        public Sate NO_SCHEMA { get; set; }
        [Annotation(5, "Can use compression protocol")]
        public Sate COMPRESS { get; set; }
        [Annotation(6, "Odbc client")]
        public Sate ODBC { get; set; }
        [Annotation(7, "Can use LOAD DATA LOCAL")]
        public Sate LOCAL_FILES { get; set; }
        [Annotation(8, "Ignore spaces before '('")]
        public Sate IGNORE_SPACE { get; set; }
        [Annotation(9, "new more secure passwords")]
        public Sate PROTOCOL_41 { get; set; }
        [Annotation(10, "This is an interactive client")]
        public Sate INTERACTIVE { get; set; }
        [Annotation(11, "Switch to SSL after handshake")]
        public Sate SSL { get; set; }
        [Annotation(12, "IGNORE sigpipes")]
        public Sate IGNORE_SIGPIPE { get; set; }
        [Annotation(13, "Client knows about transactions")]
        public Sate TRANSACTIONS { get; set; }
        [Annotation(14, "Old flag for 4.1 protocol")]
        public Sate RESERVED { get; set; }
        [Annotation(15, "New 4.1 authentication")]
        public Sate SECURE_CONNECTION { get; set; }
        public ServerCapabilities(byte[] data)
        {
            char[] _v = Convert.ToString(BitConverter.ToUInt16(data, 0), 2).PadLeft(16, '0').ToCharArray().Reverse().ToArray();
            int[] v = Array.ConvertAll(_v, _ => int.Parse(_.ToString()));
            Type type = this.GetType();
            MemberInfo[] memberInfos = type.GetMembers();
            foreach (var item in memberInfos)
            {
                Annotation annotations = item.GetCustomAttribute(typeof(Annotation)) as Annotation;
                if (annotations != null)
                {
                    type.GetProperty(item.Name).SetValue(this, (Sate)v[annotations.Id]);
                }

            }
        }

    }
}
