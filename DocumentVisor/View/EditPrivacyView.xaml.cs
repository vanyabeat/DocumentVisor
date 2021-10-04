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
    public partial class EditPrivacyView : Window
    {
        public EditPrivacyView(Privacy privacyToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            DataManageVm.SelectedPrivacy = privacyToEdit;
            DataManageVm.PrivacyName = privacyToEdit.Name;
            DataManageVm.PrivacyInfo = privacyToEdit.Info;
        }
    }
}