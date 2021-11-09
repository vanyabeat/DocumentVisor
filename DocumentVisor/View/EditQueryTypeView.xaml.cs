using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DocumentVisor.Model;
using DocumentVisor.ViewModel;

namespace DocumentVisor.View
{
    /// <summary>
    /// Логика взаимодействия для EditQueryTypeView.xaml
    /// </summary>
    public partial class EditQueryTypeView : Window
    {
        public EditQueryTypeView(QueryType queryToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            DataManageVm.SelectedQueryType = queryToEdit;
            DataManageVm.QueryTypeName = queryToEdit.Name;
            DataManageVm.QueryTypeInfo = queryToEdit.Info;
        }
    }
}
