using DocumentVisor.Model;
using DocumentVisor.ViewModel;
using System.Windows;

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