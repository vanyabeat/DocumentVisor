using DocumentVisor.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DocumentVisor.View
{
    /// <summary>
    /// ������ �������������� ��� AddQueryView.xaml
    /// </summary>
    public partial class AddQueryView : Window
    {
        public static DatePicker AllOuterSecretaryDatePicker;
        public static DatePicker AllInnerSecretaryDatePicker;
        public static DatePicker AllCentralSecretaryDatePicker;
        public static DataGrid AllExecutorPersons;
        public static DataGrid AllQueryThemes;

        private static readonly ResourceDictionary Dictionary = new ResourceDictionary
        {
            Source = new Uri(@"pack://application:,,,/Resources/StringResource.xaml")
        };

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
            QueryNameTextBox.Text = Dictionary["QueryDefaultName"].ToString() ?? string.Empty;
            DataManageVm.QueryName = Dictionary["QueryDefaultName"].ToString();
        }
    }
}
