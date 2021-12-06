using DocumentVisor.ViewModel;
using System.Windows;
using Action = DocumentVisor.Model.Action;

namespace DocumentVisor.View
{
    /// <summary>
    /// Логика взаимодействия для EditActionView.xaml
    /// </summary>
    public partial class EditActionView : Window
    {
        public EditActionView(Action actionToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            DataManageVm.SelectedAction = actionToEdit;
            DataManageVm.ActionName = actionToEdit.Name;
            DataManageVm.ActionInfo = actionToEdit.Info;
            DataManageVm.ActionNumber = actionToEdit.Number;
        }
    }
}
