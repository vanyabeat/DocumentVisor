using System;
using System.Collections.Generic;

namespace DocumentVisor.Model
{
    public class Article : IDataField, IComparable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExtendedName { get; set; }
        public string Info { get; set; }
        public ICollection<QueryArticle> QueryArticles { get; set; }

        public override string ToString()
        {
            return $"{Name} ({ExtendedName})";
        }

        public int CompareTo(object obj)
        {
            switch (obj)
            {
                case null:
                    return 1;
                case Article otherArticle:
                    return this.Id.CompareTo(otherArticle.Id);
                default:
                    throw new ArgumentException("Object is not a Article");
            }
        }
    }
}