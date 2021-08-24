namespace DocumentVisor.Model
{
    public class Person : IDataField
    {

        public string Phone { get; set; }

        public PersonType Type { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }

        public override string ToString()
        {
            return $"{nameof(Person)}: {Name} ({Phone})";
        }
    }
}
