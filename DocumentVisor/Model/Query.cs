using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentVisor.Model
{
    public class Query : IDataField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Guid { get; set; }
        public int PrivacyId { get; set; }

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


        public ICollection<QueryTheme> QueryThemes { get; set; }
        public ICollection<QueryAction> QueryActions { get; set; }
        public ICollection<QueryPerson> QueryPersons { get; set; }
        public ICollection<QueryArticle> QueryArticles { get; set; }
        public ICollection<QueryIdentifier> QueryIdentifiers { get; set; }
    }
}