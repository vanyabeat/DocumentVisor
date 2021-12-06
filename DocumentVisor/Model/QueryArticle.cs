namespace DocumentVisor.Model
{
    public class QueryArticle
    {
        public int QueryId { get; set; }
        public Query Query { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}