namespace DocumentVisor.Model
{
    public class QueryIdentifier
    {
        public int QueryId { get; set; }
        public Query Query { get; set; }
        public int IdentifierId { get; set; }
        public Identifier Identifier { get; set; }
    }
}
