using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentVisor.Model
{
    public class QueryAction
    {
        public int QueryId { get; set; }
        public Query Query { get; set; }
        public int ActionId { get; set; }
        public Action Action { get; set; }
    }
}
