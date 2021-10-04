using System;
using System.Collections.Generic;

namespace DocumentVisor.Model
{
    public class Query : IDataField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }

        public string Guid { get; set; }

        //public DocumentType Type { get; set; }
        //public Privacy Privacy { get; set; }
        //public Person SignPerson { get; set; }
        //public Person ExecutorPerson { get; set; }
        //public ICollection<Person> InnerExecutorPersons { get; set; }
        //public bool HasCd { get; set; }
        //public DateTime Date { get; set; }
        //public string DocumentNum { get; set; }
        //public DateTime SecretaryDate { get; set; }
        //public string DocumentInnerNum { get; set; }
        //public DateTime CentralSecretaryDate { get; set; }
        public Division Division { get; set; }
    }
}