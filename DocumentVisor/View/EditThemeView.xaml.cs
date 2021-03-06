using DocumentVisor.Model;
using DocumentVisor.ViewModel;
using System.Windows;

namespace DocumentVisor.View
{
    /// <summary>
    /// Логика взаимодействия для EditThemeView.xaml
    /// </summary>
    public partial class EditThemeView : Window
    {
        public EditThemeView(Theme themeToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVm();
            DataManageVm.SelectedTheme = themeToEdit;
            DataManageVm.ThemeName = themeToEdit.Name;
            DataManageVm.ThemeInfo = themeToEdit.Info;
        }
    }
}