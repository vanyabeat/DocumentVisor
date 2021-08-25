using System;
using System.Windows;
using System.Windows.Controls;
using DocumentVisor.ViewModel;

namespace DocumentVisor
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView AllPersonsView;
        public static ListView AllPersonsTypesView;
        public static TabControl MainTabControl;
        public static StackPanel StackPanelPersonTypes;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            AllPersonsView = PersonsListView;
            AllPersonsTypesView = PersonTypesListView;
            MainTabControl = MainWindowTabControl;
            PersonTypeStackPanel = StackPanelPersonTypes;
        }
    }
}