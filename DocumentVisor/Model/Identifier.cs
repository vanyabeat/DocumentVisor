using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentVisor.Model
{
    public class Identifier
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int IdentifierTypeId { get; set; }
        public override string ToString()
        {
            return $"{Content} ({IdentifierType.Name})";
        }

        [NotMapped]
        public IdentifierType IdentifierType => DataWorker.GetIdentifierTypeById(IdentifierTypeId);

        public ICollection<QueryIdentifier> QueryIdentifiers { get; set; }

    }
}
