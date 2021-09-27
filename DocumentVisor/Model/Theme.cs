namespace DocumentVisor.Model
{
    public class Theme : IDataField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }

        public override string ToString()
        {
            return $"{Name}\n({Info})";
        }
    }
}
