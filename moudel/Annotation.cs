using System;

namespace WhetherMysqlSham.moudel
{
    class Annotation : Attribute
    {

        public int Id { get; set; }
        public string Explain { get; set; }
        public Annotation(int id, string explain)
        {
            this.Id = id;
            this.Explain = explain;
        }


    }
}
