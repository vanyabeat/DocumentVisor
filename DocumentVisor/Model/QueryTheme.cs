namespace DocumentVisor.Model
{
    public class QueryTheme
    {
        public int QueryId { get; set; }
        public Query Query { get; set; }
        public int ThemeId { get; set; }
        public Theme Theme { get; set; }
    }
}