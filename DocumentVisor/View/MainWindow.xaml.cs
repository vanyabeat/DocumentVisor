﻿using System.Windows;
using System.Windows.Controls;
using DocumentVisor.ViewModel;

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
        public static DataGrid AllDocumentTypesView;
        public static TabControl MainTabControl;
        public static StackPanel StackPanelPersonTypes;

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
            AllDocumentTypesView = DocumentTypesDataGrid;
            MainTabControl = MainWindowTabControl;
            PersonTypeStackPanel = StackPanelPersonTypes;
        }
    }
}