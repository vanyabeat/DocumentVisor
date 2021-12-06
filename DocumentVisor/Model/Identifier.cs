using System.Collections.Generic;

namespace DocumentVisor.Model
{
    public class Identifier
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public IdentifierType Type { get; set; }

        public ICollection<QueryIdentifier> QueryIdentifiers { get; set; }

    }
}
