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
        public static DatePicker OuterSecretaryDatePicker;
        public static DatePicker InnerSecretaryDatePicker;
        public static DatePicker CentralSecretaryDatePicker;
        public AddQueryView()
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            OuterSecretaryDatePicker = QueryOuterSecretaryDateTimePicker;
            DataManageVm.QueryOuterSecretaryDateTime = DateTime.Now;
            InnerSecretaryDatePicker = QueryInnerSecretaryDateTimePicker;
            DataManageVm.QueryInnerSecretaryDateTime = DateTime.Now;
        }
    }
}
