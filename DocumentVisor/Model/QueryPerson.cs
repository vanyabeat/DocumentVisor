namespace DocumentVisor.Model
{
    public class QueryPerson
    {
        public int QueryId { get; set; }

        public Query Query { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
