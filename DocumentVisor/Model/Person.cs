using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentVisor.Model
{
    public class Person : IDataField, IComparable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Phone { get; set; }
        public string Rank { get; set; }
        public int TypeId { get; set; }
        public virtual PersonType Type { get; set; }

        [NotMapped] public PersonType PersonType => DataWorker.GetPersonTypeById(TypeId);

        public ICollection<QueryPerson> QueryPersons { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }

        public int CompareTo(object obj)
        {
            return obj switch
            {
                null => 1,
                Person otherPerson => this.Id.CompareTo(otherPerson.Id),
                _ => throw new ArgumentException("Object is not a Person")
            };
        }
    }
}