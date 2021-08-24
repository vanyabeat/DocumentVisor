using System.Windows.Controls;
using DocumentVisor.ViewModel;

namespace DocumentVisor.View
{
    /// <summary>
    /// Логика взаимодействия для PersonsControl.xaml
    /// </summary>
    public partial class PersonsView : UserControl
    {
        public static ListView AllPersonsView;
        public PersonsView()
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            AllPersonsView = PersonsListView;
        }
    }
}
