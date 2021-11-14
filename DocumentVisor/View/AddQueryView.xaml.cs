using System;
using System.Windows;
using System.Windows.Controls;
using DocumentVisor.ViewModel;

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
        public AddQueryView()
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            AllExecutorPersons = ExecutorPersonsDataGrid;
            AllOuterSecretaryDatePicker = QueryOuterSecretaryDateTimePicker;
            DataManageVm.QueryOuterSecretaryDateTime = DateTime.Now;
            AllInnerSecretaryDatePicker = QueryInnerSecretaryDateTimePicker;
            DataManageVm.QueryInnerSecretaryDateTime = DateTime.Now;
            AllCentralSecretaryDatePicker = QueryCentralSecretaryDateTimePicker;
            DataManageVm.QueryCentralSecretaryDateTime = DateTime.Now;
        }
    }
}
