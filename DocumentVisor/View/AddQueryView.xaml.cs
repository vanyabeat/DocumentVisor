using System;
using System.Windows;
using System.Windows.Controls;
using DocumentVisor.ViewModel;

namespace DocumentVisor.View
{
    /// <summary>
    /// Логика взаимодействия для AddQueryView.xaml
    /// </summary>
    public partial class AddQueryView : Window
    {
        public static DatePicker AllOuterSecretaryDatePicker;
        public static DatePicker AllInnerSecretaryDatePicker;
        public static DatePicker AllCentralSecretaryDatePicker;
        public static DataGrid AllExecutorPersons;
        public static DataGrid AllQueryThemes;
        public AddQueryView()
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            AllExecutorPersons = ExecutorPersonsDataGrid;
            AllQueryThemes = QueryThemesDataGrid;
            AllOuterSecretaryDatePicker = QueryOuterSecretaryDateTimePicker;
            DataManageVm.QueryOuterSecretaryDateTime = DateTime.Now;
            AllInnerSecretaryDatePicker = QueryInnerSecretaryDateTimePicker;
            DataManageVm.QueryInnerSecretaryDateTime = DateTime.Now;
            AllCentralSecretaryDatePicker = QueryCentralSecretaryDateTimePicker;
            DataManageVm.QueryCentralSecretaryDateTime = DateTime.Now;
        }
    }
}
