﻿using System.Windows;
using DocumentVisor.Model;
using DocumentVisor.ViewModel;

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
            DataManageVm.ThemeInfo = themeToEdit.Name;
            DataManageVm.ThemeName = themeToEdit.Info;
        }
    }
}