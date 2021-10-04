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
    /// Логика взаимодействия для EditDivisionView.xaml
    /// </summary>
    public partial class EditDivisionView : Window
    {
        public EditDivisionView(Division divisionToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            DataManageVm.SelectedDivision = divisionToEdit;
            DataManageVm.DivisionName = divisionToEdit.Name;
            DataManageVm.DivisionInfo = divisionToEdit.Info;
            DataManageVm.DivisionAddress = divisionToEdit.Address;
        }
    }
}