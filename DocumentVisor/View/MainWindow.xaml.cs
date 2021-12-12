using DocumentVisor.ViewModel;
using Syncfusion.UI.Xaml.Grid;
using System.Windows;
using System.Windows.Controls;

namespace DocumentVisor.View
{
    public partial class MainWindow : Window
    {
        public static DataGrid AllPersonsView;
        public static DataGrid AllPersonsTypesView;
        public static DataGrid AllPrivaciesView;
        public static DataGrid AllThemesView;
        public static DataGrid AllDivisionsView;
        public static DataGrid AllArticlesView;
        public static DataGrid AllQueryTypesView;
        public static DataGrid AllActionsView;
        public static DataGrid AllIdentifierTypesView;
        public static SfDataGrid AllQueriesView;
        public static TabControl AllMainTabControl;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            AllPersonsView = PersonsDataGrid;
            AllPersonsTypesView = PersonTypesDataGrid;
            AllPrivaciesView = PrivaciesDataGrid;
            AllThemesView = ThemesDataGrid;
            AllDivisionsView = DivisionsDataGrid;
            AllArticlesView = ArticlesDataGrid;
            AllQueryTypesView = QueryTypesDataGrid;
            AllActionsView = ActionsDataGrid;
            AllIdentifierTypesView = IdentifierTypesDataGrid;
            AllMainTabControl = MainWindowTabControl;
            AllQueriesView = QueriesDataGrid;
        }
    }
}