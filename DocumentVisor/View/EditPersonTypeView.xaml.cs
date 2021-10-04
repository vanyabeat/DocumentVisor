using System.Windows;
using DocumentVisor.Model;
using DocumentVisor.ViewModel;


namespace DocumentVisor.View
{
    /// <summary>
    /// Логика взаимодействия для EditPersonTypeView.xaml
    /// </summary>
    public partial class EditPersonTypeView : Window
    {
        public EditPersonTypeView(PersonType personTypeToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            DataManageVm.SelectedPersonType = personTypeToEdit;
            DataManageVm.PersonTypeName = personTypeToEdit.Name;
            DataManageVm.PersonTypeInfo = personTypeToEdit.Info;
        }
    }
}