using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentVisor.Model
{
    public class Query : IDataField
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public int PrivacyId { get; set; }

        public int BlobDataId => Id;


        [NotMapped]
        public virtual Privacy Privacy
        {
            get => DataWorker.GetPrivacyById(PrivacyId);
            set => PrivacyId = value.Id;
        }

        public int DivisionId { get; set; }

        [NotMapped]
        public virtual Division Division
        {
            get => DataWorker.GetDivisionById(DivisionId);
            set => DivisionId = value.Id;
        }

        public int? OutputDivisionId { get; set; }

        [NotMapped]
        public virtual Division OutputDivision
        {
            get { return OutputDivisionId == null ? null : DataWorker.GetDivisionById((int)OutputDivisionId); }
            set => DivisionId = value.Id;
        }

        public int SignPersonId { get; set; }

        [NotMapped]
        public virtual Person SignPerson
        {
            get => DataWorker.GetPersonById(SignPersonId);
            set => SignPersonId = value.Id;
        }

        public int TypeId { get; set; }

        [NotMapped]
        public virtual Type Type
        {
            get => DataWorker.GetTypeById(TypeId);
            set => TypeId = value.Id;
        }


        [NotMapped]
        public virtual bool IsCd
        {
            get => HasCd == 1;
            set => HasCd = value ? 1 : 0;
        }

        public int HasCd { get; set; }

        [NotMapped]
        public virtual bool Various
        {
            get => IsVarious == 1;
            set => IsVarious = value ? 1 : 0;
        }

        public int IsVarious { get; set; }


        public int IsEmpty { get; set; }

        [NotMapped]
        public virtual bool Empty
        {
            get => IsEmpty == 1;
            set => IsEmpty = value ? 1 : 0;
        }

        public int IsComplete { get; set; }

        [NotMapped]
        public virtual bool Complete
        {
            get => IsComplete == 1;
            set => IsComplete = value ? 1 : 0;
        }

        [NotMapped] public virtual string LinkedThemesString => string.Join(", ", LinkedThemes);
        [NotMapped] public virtual string LinkedActionsString => string.Join(", ", LinkedActions);
        [NotMapped] public virtual string LinkedArticlesString => string.Join(", ", LinkedArticles);
        [NotMapped] public virtual string LinkedPersonsString => string.Join(", ", LinkedPersons);
        [NotMapped] public virtual string LinkedIdentifiersString => string.Join(", ", LinkedIdentifiers);
        [NotMapped] public virtual List<Theme> LinkedThemes => DataWorker.GetAllQueryThemes(Id);
        [NotMapped] public virtual List<Action> LinkedActions => DataWorker.GetAllQueryAction(Id);
        [NotMapped] public virtual List<Article> LinkedArticles => DataWorker.GetAllQueryArticles(Id);
        [NotMapped] public virtual List<Person> LinkedPersons => DataWorker.GetAllQueryExecutors(Id);
        [NotMapped] public virtual List<Identifier> LinkedIdentifiers => DataWorker.GetAllQueryIdentifiers(Id);

        public ICollection<QueryTheme> QueryThemes { get; set; }
        public ICollection<QueryAction> QueryActions { get; set; }
        public ICollection<QueryPerson> QueryPersons { get; set; }
        public ICollection<QueryArticle> QueryArticles { get; set; }
        public ICollection<QueryIdentifier> QueryIdentifiers { get; set; }
        
        public string Name { get; set; }
        public string Info { get; set; }

        #region OuterSecretary

        public long OuterSecretaryDate { get; set; }

        [NotMapped]
        public virtual DateTime OuterSecretaryDateTime
        {
            get => new DateTime(OuterSecretaryDate);
            set => OuterSecretaryDate = value.Ticks;
        }

        public string OuterSecretaryNumber { get; set; }

        #endregion

        #region InnerSecretary

        public string InnerSecretaryNumber { get; set; }
        public long InnerSecretaryDate { get; set; }

        [NotMapped]
        public virtual DateTime InnerSecretaryDateTime
        {
            get => new DateTime(InnerSecretaryDate);
            set => InnerSecretaryDate = value.Ticks;
        }

        #endregion

        #region CentralSecretary

        public string CentralSecretaryNumber { get; set; }
        public long CentralSecretaryDate { get; set; }

        [NotMapped]
        public virtual DateTime CentralSecretaryDateTime
        {
            get => new DateTime(CentralSecretaryDate);
            set => CentralSecretaryDate = value.Ticks;
        }
        #endregion

        #region OutputNumberSecretary

        public string OutputSecretaryNumber { get; set; }
        public long OutputSecretaryDate { get; set; }

        [NotMapped]
        public virtual DateTime OutputSecretaryDateTime
        {
            get => new DateTime(OutputSecretaryDate);
            set => OutputSecretaryDate = value.Ticks;
        }
        #endregion
        public override string ToString()
        {
            return $"<li>{Division.Name}; {OuterSecretaryNumber}; от {OuterSecretaryDateTime:dd.MM.yyyy}; {LinkedActionsString} [исполн. {LinkedPersonsString}]</li>";
        }
    }
}