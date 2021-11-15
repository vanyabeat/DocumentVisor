using System;
using System.Collections.Generic;

namespace DocumentVisor.Model
{
    public class Theme : IDataField, IComparable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public ICollection<QueryTheme> QueryThemes { get; set; }
        public override string ToString()
        {
            return $"{Name}\n({Info})";
        }

        public int CompareTo(object obj)
        {
            return obj switch
            {
                null => 1,
                Theme otherTheme => this.Id.CompareTo(otherTheme.Id),
                _ => throw new ArgumentException("Object is not a Theme")
            };
        }
    }
}