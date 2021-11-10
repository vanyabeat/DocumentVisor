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
    /// Логика взаимодействия для AddQueryView.xaml
    /// </summary>
    public partial class AddQueryView : Window
    {
        public AddQueryView()
        {
            InitializeComponent();
            DataContext = new DataManageVm();
        }
    }
}
