using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Media;
using DocumentVisor.Model;
using DocumentVisor.View;
using static DocumentVisor.View.MainWindow;

namespace DocumentVisor.ViewModel
{
    public class DataManageVm : INotifyPropertyChanged
    {
        private static readonly ResourceDictionary Dictionary = new ResourceDictionary()
		{
			Source = new Uri(@"pack://application:,,,/Resources/StringResource.xaml")
		};
        public TabItem SelectedTabItem { get; set; }

		#region PersonTypes
		private List<PersonType> _allPersonTypes = DataWorker.GetAllPersonTypes();
        public List<PersonType> AllPersonTypes
        {
            get => _allPersonTypes;
            private set
            {
                _allPersonTypes = value;
                OnPropertyChanged(nameof(AllPersonTypes));
            }
        }
        public static PersonType SelectedPersonType { get; set; }
        public static string PersonTypeName { get; set; }
        public static string PersonTypeInfo { get; set; }

		private RelayCommand _addNewPersonType;

        public RelayCommand AddNewPersonType
        {
            get
            {
                return _addNewPersonType ?? new RelayCommand(obj =>
                {
                    var wnd = obj as Window;
                    var result = "";
                    if (PersonTypeName == null || PersonTypeName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "PersonTypeNameTextBox");
                        UpdateAllDataView();
                    }
                    else
                    {
                        result = DataWorker.CreatePersonType(PersonTypeName, PersonTypeInfo);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ClearStackPanelPersonTypesView(wnd);
                    }
                });
            }
        }

        private void UpdateAllPersonTypesView()
        {
            AllPersonTypes = DataWorker.GetAllPersonTypes();
            AllPersonsTypesView.ItemsSource = null;
            AllPersonsTypesView.Items.Clear();
            AllPersonsTypesView.ItemsSource = AllPersonTypes;
            AllPersonsTypesView.Items.Refresh();
        }

        private void ClearStackPanelPersonTypesView(Window window)
        {
            ClearTextFromStackPanelTextBox(window, "PersonTypeNameTextBox");
            ClearTextFromStackPanelTextBox(window, "PersonTypeInfoTextBox");
        }

        private void ClearStackPanelPersonView(Window window)
        {
            ClearTextFromStackPanelTextBox(window, "PersonNameTextBox");
            ClearTextFromStackPanelTextBox(window, "PersonInfoTextBox");
            ClearTextFromStackPanelTextBox(window, "PersonPhoneTextBox");
            ClearTextFromStackPanelTextBox(window, "PersonRankTextBox");
            ClearTextFromStackPanelComboBox(window, "PersonTypeComboBox");
        }

        private void ClearStackPanelPrivaciesView(Window window)
        {
            ClearTextFromStackPanelTextBox(window, "PrivacyNameTextBox");
            ClearTextFromStackPanelTextBox(window, "PrivacyInfoTextBox");
        }

        private void ClearStackPanelThemesView(Window window)
        {
            ClearTextFromStackPanelTextBox(window, "ThemeNameTextBox");
            ClearTextFromStackPanelTextBox(window, "ThemeInfoTextBox");
        }

        private RelayCommand _editPersonType;
        public RelayCommand EditPersonType
        {
            get
            {
                return _editPersonType ?? new RelayCommand(obj =>
                    {
                        var window = obj as Window;
                        if (SelectedPersonType != null)
                        {
                            var result = DataWorker.EditPersonType(SelectedPersonType, PersonTypeName, PersonTypeInfo);

                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(result);
                            window.Close();
                        }
                    }
                );
            }
        }
        #endregion
        #region Persons
        public static string PersonName { get; set; }
        public static string PersonInfo { get; set; }
        public static string PersonPhone { get; set; }
        public static string PersonRank { get; set; }
        public static PersonType PersonType { get; set; }
        public static Person SelectedPerson { get; set; }
        private List<Person> _allPersons = DataWorker.GetAllPersons();
        public List<Person> AllPersons
		{
            get => _allPersons;
			private set
			{
				_allPersons = value;
				OnPropertyChanged(nameof(AllPersons));
			}
		}
        private void UpdateAllPersonsView()
        {
            AllPersons = DataWorker.GetAllPersons();
            AllPersonsView.ItemsSource = null;
            AllPersonsView.Items.Clear();
            AllPersonsView.ItemsSource = AllPersons;
            AllPersonsView.Items.Refresh();
        }

        private RelayCommand _addNewPerson;
        public RelayCommand AddNewPerson
        {
            get
            {
                return _addNewPerson ?? new RelayCommand(obj =>
                    {
                        var wnd = obj as Window;
                        var result = "";
                        if (PersonName == null || PersonName.Replace(" ", "").Length == 0) SetRedBlockControl(wnd, "PersonNameTextBox");
                        if (PersonType == null)
                        {
                            MessageBox.Show(Dictionary["PersonTypeNeedSelect"].ToString());
                        }
                        else
                        {
                            result = DataWorker.CreatePerson(PersonName, PersonInfo, PersonPhone, PersonRank, PersonType);
                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ClearStackPanelPersonView(wnd);
                            ShowMessageToUser(result);
                        }
                    }
                );
            }
        }
        private RelayCommand _editPerson;
        public RelayCommand EditPerson
        {
            get
            {
                return _editPerson ?? new RelayCommand(obj =>
                    {
                        var window = obj as Window;
                        if (SelectedPerson != null)
                        {
                            var result = DataWorker.EditPerson(SelectedPerson, PersonName, PersonInfo, PersonPhone, PersonRank, PersonType);

                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(result);
                            window.Close();
                        }
                    }
                );
            }
        }
        #endregion
        #region Privacies
        private List<Privacy> _allPrivacies = DataWorker.GetAllPrivacies();
        public List<Privacy> AllPrivacies
        {
            get => _allPrivacies;
            private set
            {
                _allPrivacies = value;
                OnPropertyChanged(nameof(AllPrivacies));
            }
        }
        public static Privacy SelectedPrivacy { get; set; }
        public static string PrivacyName { get; set; }
        public static string PrivacyInfo { get; set; }

        private RelayCommand _addNewPrivacy;

        public RelayCommand AddNewPrivacy
        {
            get
            {
                return _addNewPrivacy ?? new RelayCommand(obj =>
                {
                    var wnd = obj as Window;
                    var result = "";
                    if (PrivacyName == null || PrivacyName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "PrivacyNameTextBox");
                        UpdateAllDataView();
                    }
                    else
                    {
                        result = DataWorker.CreatePrivacy(PrivacyName, PrivacyInfo);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ClearStackPanelPrivaciesView(wnd);
                    }
                });
            }
        }

        private RelayCommand _editPrivacy;
        public RelayCommand EditPrivacy
        {
            get
            {
                return _editPrivacy ?? new RelayCommand(obj =>
                    {
                        var window = obj as Window;
                        if (SelectedPrivacy != null)
                        {
                            var result = DataWorker.EditPrivacy(SelectedPrivacy, PrivacyName, PrivacyInfo);

                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(result);

                        }
                        window.Close();
                    }
                );
            }
        }

        private void UpdateAllPrivacyView()
        {
            AllPrivacies = DataWorker.GetAllPrivacies();
            AllPrivaciesView.ItemsSource = null;
            AllPrivaciesView.Items.Clear();
            AllPrivaciesView.ItemsSource = AllPrivacies;
            AllPrivaciesView.Items.Refresh();
        }
        #endregion
        #region Divisions
        private List<Division> _allDivisions = DataWorker.GetAllDivisions();
        public List<Division> AllDivisions
        {
            get => _allDivisions;
            private set
            {
                _allDivisions = value;
                OnPropertyChanged(nameof(AllDivisions));
            }
        }
        private RelayCommand _addNewDivision;
        public RelayCommand AddNewDivision
        {
            get
            {
                return _addNewDivision ?? new RelayCommand(obj =>
                {
                    var wnd = obj as Window;

                    if (DivisionName == null || DivisionName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "DivisionNameTextBox");
                        ShowMessageToUser(Dictionary["DivisionNameNeedToSelect"].ToString());
                    }
                    else
                    {
                        var result = DataWorker.CreateDivision(DivisionName, DivisionAddress, DivisionAddress);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ClearStackPanelThemesView(wnd);
                        ShowMessageToUser(result);
                    }
                }
                );
            }
        }
        public static string DivisionName { get; set; }
        public static string DivisionInfo { get; set; }
        public static string DivisionAddress { get; set; }
        public static Division SelectedDivision { get; set; }
        private void UpdateAllDivisionView()
        {
            AllDivisions = DataWorker.GetAllDivisions();
            AllDivisionsView.ItemsSource = null;
            AllDivisionsView.Items.Clear();
            AllDivisionsView.ItemsSource = AllDivisions;
            AllDivisionsView.Items.Refresh();
        }

        private RelayCommand _editDivision;
        public RelayCommand EditDivision
        {
            get
            {
                return _editDivision ?? new RelayCommand(obj =>
                    {
                        var window = obj as Window;
                        if (SelectedDivision != null)
                        {
                            var result = DataWorker.EditDivision(SelectedDivision, DivisionName, DivisionAddress, DivisionInfo);

                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(result);
                            window.Close();
                        }
                    }
                );
            }
        }
        #endregion
        #region Themes
        private List<Theme> _allThemes = DataWorker.GetAllThemes();
        public List<Theme> AllThemes
        {
            get => _allThemes;
            private set
            {
                _allThemes = value;
                OnPropertyChanged(nameof(AllThemes));
            }
        }
        private RelayCommand _addNewTheme;
        public RelayCommand AddNewTheme
        {
            get
            {
                return _addNewTheme ?? new RelayCommand(obj =>
                {
                    var wnd = obj as Window;

                    if (ThemeName == null || ThemeName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "ThemeNameTextBox");
                        ShowMessageToUser(Dictionary["ThemeNameNeedToSelect"].ToString());
                    }
                    else
                    {
                        var result = DataWorker.CreateTheme(ThemeName, ThemeInfo);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ClearStackPanelThemesView(wnd);
                        ShowMessageToUser(result);
                    }
                }
                );
            }
        }
        public static string ThemeName { get; set; }
        public static string ThemeInfo { get; set; }
        public static Theme SelectedTheme { get; set; }
        private void UpdateAllThemeView()
        {
            AllThemes = DataWorker.GetAllThemes();
            AllThemesView.ItemsSource = null;
            AllThemesView.Items.Clear();
            AllThemesView.ItemsSource = AllThemes;
            AllThemesView.Items.Refresh();
        }

        private RelayCommand _editTheme;
        public RelayCommand EditTheme
        {
            get
            {
                return _editTheme ?? new RelayCommand(obj =>
                {
                    var window = obj as Window;
                    if (SelectedTheme != null)
                    {
                        var result = DataWorker.EditTheme(SelectedTheme, ThemeName, ThemeInfo);

                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ShowMessageToUser(result);
                        window.Close();
                    }
                }
                );
            }
        }


        #endregion

        #region Queries
        private List<Query> _allQueries = DataWorker.GetAllQueries();
        public List<Query> AllQueries
        {
            get => _allQueries;
            private set
            {
                _allQueries = value;
                OnPropertyChanged(nameof(AllQueries));
            }
        }


        #endregion
        #region Updates
        private void UpdateAllDataView()
        {
            UpdateAllPersonsView();
            UpdateAllPersonTypesView();
            UpdateAllPrivacyView();
            UpdateAllThemeView();
            UpdateAllDivisionView();
        }

        #endregion
        #region Deletes
        private RelayCommand _deleteItem;

        public RelayCommand DeleteItem
        {
            get
            {

                return _deleteItem ?? new RelayCommand(obj =>
                {
                    var result = Dictionary["ObjectNotFound"].ToString();
                    var wnd = obj as Window;
                    switch (SelectedTabItem.Name)
                    {
                        case "PersonsTab" when SelectedPerson != null:
                            result = DataWorker.DeletePerson(SelectedPerson);
                            UpdateAllDataView();
                            ClearStackPanelPersonView(wnd);
                            break;
                        case "PersonTypesTab" when SelectedPersonType != null:
                            result = DataWorker.DeletePersonType(SelectedPersonType);
                            UpdateAllDataView();
                            ClearStackPanelPersonTypesView(wnd);
                            break;
                        case "PrivaciesTab" when SelectedPrivacy != null:
                            result = DataWorker.DeletePrivacy(SelectedPrivacy);
                            UpdateAllDataView();
                            ClearStackPanelPrivaciesView(wnd);
                            break;
                        case "ThemesTab" when SelectedTheme != null:
                            result = DataWorker.DeleteTheme(SelectedTheme);
                            UpdateAllDataView();
                            ClearStackPanelThemesView(wnd);
                            break;
                        case "DivisionsTab" when SelectedDivision != null:
                            result = DataWorker.DeleteDivision(SelectedDivision);
                            UpdateAllDataView();
                            ClearStackPanelThemesView(wnd);
                            break;
                    }
                    // upd
                    SetNullValuesToProperties();
                    ShowMessageToUser(result);
                });
            }
        }
        private void SetNullValuesToProperties()
        {
            //Person
            PersonName = null;
            PersonInfo = null;
            PersonPhone = null;
            PersonType = null;
            // PersonType
            PersonTypeName = null;
            PersonTypeInfo = null;
            // Privacy
            PrivacyInfo = null;
            PrivacyName = null;
            // Themes
            ThemeInfo = null;
            ThemeName = null;
            // Divisions
            DivisionName = null;
            DivisionInfo = null;
            DivisionAddress = null;
        }

        #endregion
        #region Utils
        private void SetRedBlockControl(Window window, string blockName)
        {
            var block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Crimson;
        }

        private void ClearTextFromStackPanelTextBox(Window window, string blockName)
        {
            var block = window.FindName(blockName) as TextBox;
            block.Clear();
        }

        private void ClearTextFromStackPanelComboBox(Window window, string blockName)
        {
            var block = window.FindName(blockName) as ComboBox;
            block.SelectedItem = null;
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        private void ShowMessageToUser(string message)
        {
            var messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        }

        private void OpenEditPersonTypeViewMethod(PersonType personType)
        {
            var editDepartmentWindow = new EditPersonTypeView(personType);
            SetCenterPositionAndOpen(editDepartmentWindow);
        }
        private void OpenEditPrivacyViewMethod(Privacy privacy)
        {
            var editPrivacyWindow = new EditPrivacyView(privacy);
            SetCenterPositionAndOpen(editPrivacyWindow);
        }

        private void OpenEditPersonViewMethod(Person person)
        {
            var editPersonWindow = new EditPersonView(person);
            SetCenterPositionAndOpen(editPersonWindow);
        }

        private void OpenEditThemeViewMethod(Theme theme)
        {
            var editThemeWindow = new EditThemeView(theme);
            SetCenterPositionAndOpen(editThemeWindow);
        }

        private void OpenEditDivisionViewMethod(Division div)
        {
            var editDivisionWindow = new EditDivisionView(div);
            SetCenterPositionAndOpen(editDivisionWindow);
        }

        private RelayCommand _openEditItemWnd;
        public RelayCommand OpenEditItemWnd
        {
            get
            {
                return _openEditItemWnd ?? new RelayCommand(obj =>
                    {
                        switch (SelectedTabItem.Name)
                        {
                            case "PersonsTab" when SelectedPerson != null:
                                OpenEditPersonViewMethod(SelectedPerson);
                                return;
                            case "PersonTypesTab" when SelectedPersonType != null:
                                OpenEditPersonTypeViewMethod(SelectedPersonType);
                                return;
                            case "PrivaciesTab" when SelectedPrivacy != null:
                                OpenEditPrivacyViewMethod(SelectedPrivacy);
                                return;
                            case "ThemesTab" when SelectedTheme != null:
                                OpenEditThemeViewMethod(SelectedTheme);
                                return;
                            case "DivisionsTab" when SelectedDivision != null:
                                OpenEditDivisionViewMethod(SelectedDivision);
                                return;
                            default:
                                ShowMessageToUser(Dictionary["PleaseSelectNeedleItem"].ToString());
                                return;
                        }
                    }
                );
            }
        }
        #endregion
        #region MVVM
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

	}
}
