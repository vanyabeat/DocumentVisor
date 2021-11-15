using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentVisor.Model
{
    public class QueryPerson
    {
        public int QueryId { get; set; }

        public virtual Query Query
        {
            get => DataWorker.GetQueryById(QueryId);
            set => QueryId = value.Id;
        }

        public int PersonId { get; set; }
        public virtual Person Person
        {
            get => DataWorker.GetPersonById(PersonId);
            set => PersonId = value.Id;
        }
    }
}
