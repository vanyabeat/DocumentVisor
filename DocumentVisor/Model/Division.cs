namespace DocumentVisor.Model
{
    public class Division : IDataField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Info { get; set; }

        public override string ToString()
        {
            return $"{Name}\n{Info}";
        }
    }
}