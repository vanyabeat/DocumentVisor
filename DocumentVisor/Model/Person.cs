﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentVisor.Model
{
    public class Person : IDataField
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
            return $"{Name}\n({Rank})";
        }
    }
}