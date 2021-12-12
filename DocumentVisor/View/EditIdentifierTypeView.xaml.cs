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
using DocumentVisor.ViewModel;

namespace DocumentVisor.View
{
    /// <summary>
    /// Interaction logic for EditIdentifierTypeView.xaml
    /// </summary>
    public partial class EditIdentifierTypeView : Window
    {
        public EditIdentifierTypeView(IdentifierType idT)
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            DataManageVm.SelectedIdentifierType = idT;
            DataManageVm.IdentifierTypeName = idT.Name;
            DataManageVm.IdentifierTypeInfo = idT.Info;
        }
    }
}
