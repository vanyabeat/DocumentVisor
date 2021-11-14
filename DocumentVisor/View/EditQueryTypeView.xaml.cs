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
using Type = DocumentVisor.Model.Type;

namespace DocumentVisor.View
{
    /// <summary>
    /// Логика взаимодействия для EditQueryTypeView.xaml
    /// </summary>
    public partial class EditQueryTypeView : Window
    {
        public EditQueryTypeView(Type toEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            DataManageVm.SelectedType = toEdit;
            DataManageVm.QueryTypeName = toEdit.Name;
            DataManageVm.QueryTypeInfo = toEdit.Info;
        }
    }
}
