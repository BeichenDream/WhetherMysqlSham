using System;
using System.Reflection;
using System.Linq;
namespace WhetherMysqlSham.moudel.ServerGreeting
{
    class ExtendedServerCapabilities
    {
        [Annotation(0, "Multiple statements")]
        public Sate Multiple_Statements { get; set; }
        [Annotation(1, "Multiple results")]
        public Sate Multiple_Results { get; set; }
        [Annotation(2, "PS Multiple results")]
        public Sate PS_Multiple_Results { get; set; }
        [Annotation(3, "Plugin Auth")]
        public Sate Plugin_Auth { get; set; }
        [Annotation(4, "Connect attrs")]
        public Sate Connect_Attrs { get; set; }

        [Annotation(5, "Plugin Auth LENENC Client Data")]
        public Sate Plugin_Auth_LENENC_Client_Data { get; set; }
        [Annotation(6, "Client can handle expired passwords")]
        public Sate Client_Can_Handle_Expired_Passwords { get; set; }
        [Annotation(7, "Session variable tracking")]
        public Sate Session_Variable_Tracking { get; set; }
        [Annotation(8, "Deprecate EOF")]
        public Sate Deprecate_EOF { get; set; }


        public ExtendedServerCapabilities(byte[] data)
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
