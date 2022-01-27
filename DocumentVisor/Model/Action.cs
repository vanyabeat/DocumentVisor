using System;
using System.Collections.Generic;

namespace DocumentVisor.Model
{
    public class Action : IDataField, IComparable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Info { get; set; }
        public ICollection<QueryAction> QueryActions { get; set; }
        public override string ToString()
        {
            var result = "";
            if (!string.IsNullOrEmpty(Name))
            {
                result += Name;
            }
            if (!string.IsNullOrEmpty(Number))
            {
                result += $" №{Number}";
            }
            if (!string.IsNullOrEmpty(Info))
            {
                result += $" ({Info})";
            }
            return result;
        }

        public int CompareTo(object obj)
        {
            return obj switch
            {
                null => 1,
                Action otherAction => this.Id.CompareTo(otherAction.Id),
                _ => throw new ArgumentException("Object is not a Action")
            };
        }
    }
}
