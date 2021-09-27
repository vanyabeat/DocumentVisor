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
    /// Логика взаимодействия для EditPersonView.xaml
    /// </summary>
    public partial class EditPersonView : Window
    {
        public EditPersonView(Person personToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            DataManageVm.SelectedPerson = personToEdit;
            DataManageVm.PersonName = personToEdit.Name;
            DataManageVm.PersonPhone = personToEdit.Phone;
            DataManageVm.PersonInfo = personToEdit.Info;
            DataManageVm.PersonType = personToEdit.PersonType;
        }
    }
}
