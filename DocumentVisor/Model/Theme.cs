using System;
using System.Collections;
using System.Collections.Generic;

namespace DocumentVisor.Model
{
    public class Theme : IDataField, IComparable<Theme>, IComparer<Theme>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public ICollection<QueryTheme> QueryThemes { get; set; }
        public override string ToString()
        {
            return $"{Name} ({Info})";
        }

        public int CompareTo(Theme other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Id.CompareTo(other.Id);
        }

        public int Compare(Theme x, Theme y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            return x.Id.CompareTo(y.Id);
        }
    }
}