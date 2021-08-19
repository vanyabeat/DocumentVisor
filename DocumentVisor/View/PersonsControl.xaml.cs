using System.Windows.Controls;
using DocumentVisor.ViewModel;

namespace DocumentVisor.View
{
    /// <summary>
    /// Логика взаимодействия для PersonsControl.xaml
    /// </summary>
    public partial class PersonsControl : UserControl
    {
        public PersonsControl()
        {
            InitializeComponent();
            DataContext = new DataManageVm();
        }
    }
}
