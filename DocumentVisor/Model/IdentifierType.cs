using DocumentVisor.Model;

namespace DocumentVisor
{
    public class IdentifierType : IDataField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }

        public override string ToString()
        {
            return $"{Name}\n({Info})";
        }

        public object ToObject()
        {
            return new
            {
                Id = Id,
                Info = Info,
                Name = Name
            };
        }
    }
}
