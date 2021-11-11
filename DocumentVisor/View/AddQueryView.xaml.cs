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
        public AddQueryView()
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            OuterSecretaryDatePicker = QueryOuterSecretaryDateTimePicker;
            DataManageVm.QueryOuterSecretaryDateTime = DateTime.Now;
        }
    }
}
