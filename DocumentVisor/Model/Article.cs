using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentVisor.Model
{
    public class Article : IDataField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExtendedName { get; set; }
        public string Info { get; set; }

        public override string ToString()
        {
            return $"{Name} ({ExtendedName})";
        }
    }
}