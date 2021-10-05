using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentVisor.Model
{
    public class Action : IDataField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Info { get; set; }
        public ICollection<QueryAction> QueryActions { get; set; }
        public override string ToString()
        {
            return $"{Name} ({Number})";
        }
    }
}
