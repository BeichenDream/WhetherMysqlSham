using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WhetherMysqlSham.moudel.ServerGreeting
{
    class ServerStatus
    {
        [Annotation(0, "In transaction")]
        public Sate STATUS_IN_TRANS { get; set; }
        [Annotation(1, "AUTO_COMMIT")]
        public Sate STATUS_AUTOCOMMIT { get; set; }
        [Annotation(2, "Multi query / Unused")]
        public Sate Multi_Query_Unused { get; set; }
        [Annotation(3, "More results")]
        public Sate MORE_RESULTS_EXISTS { get; set; }
        [Annotation(4, "Bad index used")]
        public Sate STATUS_NO_GOOD_INDEX_USED { get; set; }
        [Annotation(5, "No index used")]
        public Sate STATUS_NO_INDEX_USED { get; set; }
        [Annotation(6, "No index used")]
        public Sate STATUS_CURSOR_EXISTS { get; set; }
        [Annotation(7, "Last row sent")]
        public Sate STATUS_LAST_ROW_SENT { get; set; }
        [Annotation(8, "Database dropped")]
        public Sate STATUS_DB_DROPPED { get; set; }
        [Annotation(9, "No backslash escapes")]
        public Sate STATUS_NO_BACKSLASH_ESCAPES { get; set; }
        [Annotation(10, "Metadata changed")]
        public Sate STATUS_METADATA_CHANGED { get; set; }
        [Annotation(11, "Query was slow")]
        public Sate QUERY_WAS_SLOW { get; set; }
        [Annotation(12, "PS Out Params")]
        public Sate PS_OUT_PARAMS { get; set; }
        [Annotation(13, "In Trans Readonly")]
        public Sate STATUS_IN_TRANS_READONLY { get; set; }
        [Annotation(14, "Session state changed")]
        public Sate SESSION_STATE_CHANGED { get; set; }
        public ServerStatus(byte[] data)
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
