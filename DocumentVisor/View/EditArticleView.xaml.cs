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
    /// Логика взаимодействия для EditArticleView.xaml
    /// </summary>
    public partial class EditArticleView : Window
    {
        public EditArticleView(Article articleToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            DataManageVm.SelectedArticle = articleToEdit;
            DataManageVm.ArticleName = articleToEdit.Name;
            DataManageVm.ArticleInfo = articleToEdit.Info;
            DataManageVm.ArticleExtendedName = articleToEdit.ExtendedName;
        }
    }
}
