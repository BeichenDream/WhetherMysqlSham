using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhetherMysqlSham.plugin
{
    public interface Plugin
    {
        bool IsSham();
        void Init(string host, int port);
    }
}
