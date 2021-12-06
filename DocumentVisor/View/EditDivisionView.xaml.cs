using DocumentVisor.Model;
using DocumentVisor.ViewModel;
using System.Windows;

namespace DocumentVisor.View
{
    /// <summary>
    /// Логика взаимодействия для EditDivisionView.xaml
    /// </summary>
    public partial class EditDivisionView : Window
    {
        public EditDivisionView(Division divisionToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            DataManageVm.SelectedDivision = divisionToEdit;
            DataManageVm.DivisionName = divisionToEdit.Name;
            DataManageVm.DivisionInfo = divisionToEdit.Info;
            DataManageVm.DivisionAddress = divisionToEdit.Address;
        }
    }
}