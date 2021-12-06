using System.Collections.Generic;
using DocumentVisor.Model;
using DocumentVisor.ViewModel;
using System.Windows;

namespace DocumentVisor.View
{
    public partial class EditQueryView : Window
    {
        public EditQueryView(Query queryToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            DataManageVm.SelectedQuery = queryToEdit;
            DataManageVm.QueryName = queryToEdit.Name;
            DataManageVm.QueryGuid = queryToEdit.Guid;
            DataManageVm.QueryInfo = queryToEdit.Info;
            DataManageVm.QueryPrivacy = queryToEdit.Privacy;
            DataManageVm.QueryDivision = queryToEdit.Division;
            DataManageVm.QuerySignPerson = queryToEdit.SignPerson;
            DataManageVm.QueryType = queryToEdit.Type;
            DataManageVm.QueryOuterSecretaryDateTime = queryToEdit.OuterSecretaryDateTime;
            DataManageVm.QueryOuterSecretaryNumber = queryToEdit.OuterSecretaryNumber;
            DataManageVm.QueryInnerSecretaryDateTime = queryToEdit.InnerSecretaryDateTime;
            DataManageVm.QueryInnerSecretaryNumber = queryToEdit.InnerSecretaryNumber;
            DataManageVm.QueryCentralSecretaryDateTime = queryToEdit.CentralSecretaryDateTime;
            DataManageVm.QueryCentralSecretaryNumber = queryToEdit.CentralSecretaryNumber;
            DataManageVm.QueryEmpty = queryToEdit.Empty;
            DataManageVm.QueryVarious = queryToEdit.Various;
            DataManageVm.QueryHasCd = queryToEdit.IsCd;
            (DataContext as DataManageVm).QueryExecutorPersons = new SortedSet<Person>(queryToEdit.LinkedPersons);
            (DataContext as DataManageVm).QueryActions = new SortedSet<Action>(queryToEdit.LinkedActions);
            (DataContext as DataManageVm).QueryArticles = new SortedSet<Article>(queryToEdit.LinkedArticles);
            (DataContext as DataManageVm).QueryThemes = new SortedSet<Theme>(queryToEdit.LinkedThemes);
            DivisionCombobox.SelectedIndex =
                (DataContext as DataManageVm).AllDivisions.FindIndex(x => x.Id == queryToEdit.DivisionId);
            PrivacyCombobox.SelectedIndex =
                (DataContext as DataManageVm).AllPrivacies.FindIndex(x => x.Id == queryToEdit.PrivacyId);
            DocTypeCombobox.SelectedIndex =
                (DataContext as DataManageVm).AllQueryTypes.FindIndex(x => x.Id == queryToEdit.TypeId);
            SignPersonCombobox.SelectedIndex =
                (DataContext as DataManageVm).AllPersons.FindIndex(x => x.Id == queryToEdit.SignPersonId);
        }

    }
}
