using DocumentVisor.Model;

namespace DocumentVisor
{
    public class IdentifierType : IDataField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
    }
}
