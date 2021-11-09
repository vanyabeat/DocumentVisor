using System;
using System.Collections.Generic;

namespace DocumentVisor.Model
{
    public class Query : IDataField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Guid { get; set; }
        public Privacy Privacy { get; set; }
        public Division Division { get; set; }
        public Person SignPerson { get; set; }
        public QueryType Type { get; set; }
        public string OuterSecretaryNumber { get; set; }
        public long OuterSecretaryDate { get; set; }
        public string InnerSecretaryNumber { get; set; }
        public long InnerSecretaryDate { get; set; }
        public string CentralSecretaryNumber { get; set; }
        public long CentralSecretaryDate { get; set; }
        public ICollection<QueryTheme> QueryThemes { get; set; }
        public ICollection<QueryAction> QueryActions { get; set; }
        public ICollection<QueryPerson> QueryPersons { get; set; }
        public ICollection<QueryArticle> QueryArticles { get; set; }
        public ICollection<QueryIdentifier> QueryIdentifiers { get; set; }
        public int HasCd { get; set; }
        public bool IsVarious { get; set; }
        public bool IsEmpty { get; set; }
    }
}