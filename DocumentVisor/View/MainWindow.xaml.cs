using System.Windows;
using System.Windows.Controls;
using DocumentVisor.ViewModel;

namespace DocumentVisor.View
{
    public partial class MainWindow : Window
    {
        public static ListView AllPersonsView;
        public static DataGrid AllPersonsTypesView;
        public static TabControl MainTabControl;
        public static StackPanel StackPanelPersonTypes;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            AllPersonsView = PersonsListView;
            AllPersonsTypesView = PersonTypesDataGrid;
            MainTabControl = MainWindowTabControl;
            PersonTypeStackPanel = StackPanelPersonTypes;
        }

    }
}