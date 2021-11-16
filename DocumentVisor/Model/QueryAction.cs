namespace DocumentVisor.Model
{
    public class QueryAction
    {
        public int QueryId { get; set; }
        public virtual Query Query { get; set; }
        public int ActionId { get; set; }
        public Action Action { get; set; }
    }
}