using DocumentVisor.ViewModel;
using System.Windows;
using Type = DocumentVisor.Model.Type;

namespace DocumentVisor.View
{
    /// <summary>
    /// Логика взаимодействия для EditQueryTypeView.xaml
    /// </summary>
    public partial class EditQueryTypeView : Window
    {
        public EditQueryTypeView(Type toEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            DataManageVm.SelectedType = toEdit;
            DataManageVm.QueryTypeName = toEdit.Name;
            DataManageVm.QueryTypeInfo = toEdit.Info;
        }
    }
}
