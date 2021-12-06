using System.Collections.Generic;
using System.Linq;
using DocumentVisor.Model;
using DocumentVisor.ViewModel;
using System.Windows;
using System.Windows.Controls;
using Syncfusion.Data.Extensions;

namespace DocumentVisor.View
{
    public partial class EditPersonView : Window
    {
        public EditPersonView(Person personToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            DataManageVm.SelectedPerson = personToEdit;
            DataManageVm.PersonName = personToEdit.Name;
            DataManageVm.PersonPhone = personToEdit.Phone;
            DataManageVm.PersonInfo = personToEdit.Info;
            DataManageVm.PersonRank = personToEdit.Rank;
            DataManageVm.PersonType = personToEdit.PersonType;
            PersonTypeCombobox.SelectedIndex =
                ((DataManageVm)DataContext).AllPersonTypes.IndexOf(((DataManageVm)DataContext).AllPersonTypes.FirstOrDefault(x => x.Id == personToEdit.PersonType.Id));

        }
    }
}    