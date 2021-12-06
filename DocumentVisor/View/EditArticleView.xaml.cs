using DocumentVisor.Model;
using DocumentVisor.ViewModel;
using System.Windows;

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
