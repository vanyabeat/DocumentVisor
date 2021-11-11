﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DocumentVisor.Model;
using DocumentVisor.View;
using static System.Guid;
using static DocumentVisor.View.MainWindow;
using Action = DocumentVisor.Model.Action;

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

        private RelayCommand _addNewPersonType = null;

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



        private RelayCommand _editPersonType = null;

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

        private RelayCommand _addNewPerson = null;

        public RelayCommand AddNewPerson
        {
            get
            {
                return _addNewPerson ?? new RelayCommand(obj =>
                    {
                        var wnd = obj as Window;
                        var result = "";
                        if (PersonName == null || PersonName.Replace(" ", "").Length == 0)
                        {
                            SetRedBlockControl(wnd, "PersonNameTextBox");
                            ShowMessageToUser(Dictionary["PersonTypeNeedSelect"].ToString());

                        }
                        else
                        {
                            result = DataWorker.CreatePerson(PersonName, PersonInfo, PersonPhone, PersonRank,
                                PersonType);
                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ClearStackPanelPersonView(wnd);
                            ShowMessageToUser(result);
                        }
                    }
                );
            }
        }

        private RelayCommand _editPerson = null;

        public RelayCommand EditPerson
        {
            get
            {
                return _editPerson ?? new RelayCommand(obj =>
                    {
                        var window = obj as Window;
                        if (SelectedPerson != null)
                        {
                            var result = DataWorker.EditPerson(SelectedPerson, PersonName, PersonInfo, PersonPhone,
                                PersonRank, PersonType);

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

        private RelayCommand _addNewPrivacy = null;

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

        private RelayCommand _editPrivacy = null;

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

        private RelayCommand _addNewDivision = null;

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
                            var result = DataWorker.CreateDivision(DivisionName, DivisionAddress, DivisionInfo);
                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ClearStackPanelDivisionsView(wnd);
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

        private RelayCommand _editDivision = null;

        public RelayCommand EditDivision
        {
            get
            {
                return _editDivision ?? new RelayCommand(obj =>
                    {
                        var window = obj as Window;
                        if (SelectedDivision != null)
                        {
                            var result = DataWorker.EditDivision(SelectedDivision, DivisionName, DivisionAddress,
                                DivisionInfo);

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

        private RelayCommand _addNewTheme = null;

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

        private RelayCommand _editTheme = null;

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

        #region Articles

        private List<Article> _allArticles = DataWorker.GetAllArticles();

        public List<Article> AllArticles
        {
            get => _allArticles;
            private set
            {
                _allArticles = value;
                OnPropertyChanged(nameof(AllArticles));
            }
        }

        private RelayCommand _addNewArticle = null;

        public RelayCommand AddNewArticle
        {
            get
            {
                return _addNewArticle ?? new RelayCommand(obj =>
                {
                    var wnd = obj as Window;

                    if (ArticleName == null || ArticleName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "ArticleNameTextBox");
                        ShowMessageToUser(Dictionary["ArticleNameNeedToSelect"].ToString());
                    }
                    else
                    {
                        var result = DataWorker.CreateArticle(ArticleName, ArticleExtendedName, ArticleInfo);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ClearStackPanelArticlesView(wnd);
                        ShowMessageToUser(result);
                    }
                }
                );
            }
        }

        public static string ArticleName { get; set; }
        public static string ArticleInfo { get; set; }
        public static string ArticleExtendedName { get; set; }
        public static Article SelectedArticle { get; set; }

        private void UpdateAllArticleView()
        {
            AllArticles = DataWorker.GetAllArticles();
            AllArticlesView.ItemsSource = null;
            AllArticlesView.Items.Clear();
            AllArticlesView.ItemsSource = AllArticles;
            AllArticlesView.Items.Refresh();
        }

        private RelayCommand _editArticle = null;

        public RelayCommand EditArticle
        {
            get
            {
                return _editArticle ?? new RelayCommand(obj =>
                {
                    var window = obj as Window;
                    if (SelectedArticle != null)
                    {
                        var result = DataWorker.EditArticle(SelectedArticle, ArticleName, ArticleExtendedName, ArticleInfo);

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

        #region QueryType

        private List<QueryType> _allQueryTypes = DataWorker.GetAllQueryTypes();

        public List<QueryType> AllQueryTypes
        {
            get => _allQueryTypes;
            private set
            {
                _allQueryTypes = value;
                OnPropertyChanged(nameof(AllQueryTypes));
            }
        }

        private RelayCommand _addNewQueryType = null;

        public RelayCommand AddNewQueryType
        {
            get
            {
                return _addNewQueryType ?? new RelayCommand(obj =>
                {
                    var wnd = obj as Window;

                    if (QueryTypeName == null || QueryTypeName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "QueryTypeNameTextBox");
                        ShowMessageToUser(Dictionary["QueryTypeNameNeedToSelect"].ToString());
                    }
                    else
                    {
                        var result = DataWorker.CreateQueryType(QueryTypeName, QueryTypeInfo);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ClearStackPanelQueryTypeView(wnd);
                        ShowMessageToUser(result);
                    }
                }
                );
            }
        }

        public static string QueryTypeInfo { get; set; }
        public static string QueryTypeName { get; set; }
        public static QueryType SelectedQueryType { get; set; }

        private void UpdateAllQueryTypesView()
        {
            AllQueryTypes = DataWorker.GetAllQueryTypes();
            AllQueryTypesView.ItemsSource = null;
            AllQueryTypesView.Items.Clear();
            AllQueryTypesView.ItemsSource = AllQueryTypes;
            AllQueryTypesView.Items.Refresh();
        }

        private RelayCommand _editQueryType = null;

        public RelayCommand EditQueryType
        {
            get
            {
                return _editQueryType ?? new RelayCommand(obj =>
                {
                    var window = obj as Window;
                    if (SelectedQueryType != null)
                    {
                        var result = DataWorker.EditQueryType(SelectedQueryType, QueryTypeName, QueryTypeInfo);

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

        #region Actions

        private List<Action> _allActions = DataWorker.GetAllActions();

        public List<Action> AllActions
        {
            get => _allActions;
            private set
            {
                _allActions = value;
                OnPropertyChanged(nameof(AllActions));
            }
        }

        private RelayCommand _addNewAction = null;

        public RelayCommand AddNewAction
        {
            get
            {
                return _addNewAction ?? new RelayCommand(obj =>
                {
                    var wnd = obj as Window;

                    if ( ActionName == null || ActionName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "ActionNameTextBox");
                        ShowMessageToUser(Dictionary["ActionNameNeedToSelect"].ToString());
                    }
                    else
                    {
                        var result = DataWorker.CreateAction(ActionName, ActionNumber, ActionInfo);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ClearStackPanelActionsView(wnd);
                        ShowMessageToUser(result);
                    }
                }
                );
            }
        }

        public static string ActionName { get; set; }
        public static string ActionInfo { get; set; }
        public static string ActionNumber { get; set; }
        public static Action SelectedAction { get; set; }

        private void UpdateAllActionView()
        {
            AllActions = DataWorker.GetAllActions();
            AllActionsView.ItemsSource = null;
            AllActionsView.Items.Clear();
            AllActionsView.ItemsSource = AllActions;
            AllActionsView.Items.Refresh();
        }

        private RelayCommand _editAction = null;

        public RelayCommand EditAction
        {
            get
            {
                return _editAction ?? new RelayCommand(obj =>
                {
                    var window = obj as Window;
                    if (SelectedAction != null)
                    {
                        var result = DataWorker.EditAction(SelectedAction, ActionName, ActionNumber, ActionInfo);

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
        public static string QueryName { get; set; }
        public static string QueryGuid { get; set; }
        public static Privacy QueryPrivacy { get; set; }

        public static Division QueryDivision { get; set; }
        public static Person QuerySignPerson { get; set; }
        public static QueryType QueryType { get; set; }

        public static DateTime QueryOuterSecretaryDateTime { get; set; }
        public static string QueryOuterSecretaryNumber { get; set; }
        

        private RelayCommand _createGuid = null;

        public RelayCommand CreateGuid   
        {
            get
            {
                return _createGuid ?? new RelayCommand(obj =>
                    {
                        var window = obj as Window;
                        QueryGuid = GenerateRandomGuid();
                        if (window?.FindName("QueryGuidTextBox") is TextBox textBox) textBox.Text = QueryGuid;
                    }
                );
            }
        }
        private List<Query> _allQueries = DataWorker.GetAllQueries();
        #endregion

        #region Updates

        private void UpdateAllDataView()
        {
            UpdateAllPersonsView();
            UpdateAllPersonTypesView();
            UpdateAllPrivacyView();
            UpdateAllThemeView();
            UpdateAllDivisionView();
            UpdateAllQueryTypesView();
            UpdateAllActionView();
            UpdateAllArticleView();
        }

        #endregion

        #region Deletes

        private RelayCommand _deleteItem = null;

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
                            ClearStackPanelDivisionsView(wnd);
                            break;
                        case "ArticlesTab" when SelectedArticle != null:
                            result = DataWorker.DeleteArticle(SelectedArticle);
                            UpdateAllDataView();
                            ClearStackPanelArticlesView(wnd);
                            break;
                        case "QueryTypesTab" when SelectedQueryType != null:
                            result = DataWorker.DeleteQueryType(SelectedQueryType);
                            UpdateAllDataView();
                            ClearStackPanelQueryTypeView(wnd);
                            break;
                        case "ActionsTab" when SelectedAction != null:
                            result = DataWorker.DeleteAction(SelectedAction);
                            UpdateAllDataView();
                            ClearStackPanelActionsView(wnd);
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
            // Article
            ArticleName = null;
            ArticleExtendedName = null;
            ArticleInfo = null;
            // QueryType
            QueryTypeInfo = null;
            QueryTypeName = null;
            // Actions
            ActionInfo = null;
            ActionName = null;
            ActionNumber = null;
        }

        #endregion

        #region Utils   

        private string GenerateRandomGuid()
        {
            var time = DateTime.Now;
            var guid = NewGuid().ToString().Substring(0, 7);
            return $"{time.Day}{time.Month}{time.Year.ToString().Substring(1,3)}_{guid}";

        }
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
            var wnd = new MessageView(message);
            SetCenterPositionAndOpen(wnd);
        }

        private void OpenEditPersonTypeViewMethod(PersonType personType)
        {
            var wnd = new EditPersonTypeView(personType);
            SetCenterPositionAndOpen(wnd);
        }

        private void OpenEditPrivacyViewMethod(Privacy privacy)
        {
            var wnd = new EditPrivacyView(privacy);
            SetCenterPositionAndOpen(wnd);
        }

        private void OpenEditPersonViewMethod(Person person)
        {
            var wnd = new EditPersonView(person);
            SetCenterPositionAndOpen(wnd);
        }

        private void OpenEditThemeViewMethod(Theme theme)
        {
            var wnd = new EditThemeView(theme);
            SetCenterPositionAndOpen(wnd);
        }

        private void OpenEditDivisionViewMethod(Division div)
        {
            var wnd = new EditDivisionView(div);
            SetCenterPositionAndOpen(wnd);
        }


        private void OpenEditArticleViewMethod(Article article)
        {
            var wnd = new EditArticleView(article);
            SetCenterPositionAndOpen(wnd);
        }

        private void OpenEditQueryTypeViewMethod(QueryType queryType)
        {
            var wnd = new EditQueryTypeView(queryType);
            SetCenterPositionAndOpen(wnd);
        }
        private void OpenEditActionViewMethod(Action action)
        {
            var wnd = new EditActionView(action);
            SetCenterPositionAndOpen(wnd);
        }

        private void OpenAddQueryViewMethod()
        {
            var wnd = new AddQueryView();
            SetCenterPositionAndOpen(wnd);
        }

        private RelayCommand _openAddQueryWnd = null;

        public RelayCommand OpenAddQueryWnd
        {
            get
            {
                return _openAddQueryWnd ?? new RelayCommand(obj =>
                    {
                        switch (SelectedTabItem.Name)
                        {
                            case "QueriesTab":
                                OpenAddQueryViewMethod();
                                return;
                            default:
                                ShowMessageToUser(Dictionary["PleaseSelectNeedleItem"].ToString());
                                return;
                        }
                    }
                );
            }
        }
        private RelayCommand _openEditItemWnd = null;

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
                            case "ArticlesTab" when SelectedArticle != null:
                                OpenEditArticleViewMethod(SelectedArticle);
                                return;
                            case "QueryTypesTab" when SelectedQueryType != null:
                                OpenEditQueryTypeViewMethod(SelectedQueryType);
                                return;
                            case "ActionsTab" when SelectedAction != null:
                                OpenEditActionViewMethod(SelectedAction);
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

        #region Flushes
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

        private void ClearStackPanelDivisionsView(Window window)
        {
            ClearTextFromStackPanelTextBox(window, "DivisionNameTextBox");
            ClearTextFromStackPanelTextBox(window, "DivisionInfoTextBox");
            ClearTextFromStackPanelTextBox(window, "DivisionAddressTextBox");
        }

        private void ClearStackPanelArticlesView(Window window)
        {
            ClearTextFromStackPanelTextBox(window, "ArticleNameTextBox");
            ClearTextFromStackPanelTextBox(window, "ArticleInfoTextBox");
            ClearTextFromStackPanelTextBox(window, "ArticleExtendedNameTextBox");
        }

        private void ClearStackPanelQueryTypeView(Window window)
        {
            ClearTextFromStackPanelTextBox(window, "QueryTypeNameTextBox");
            ClearTextFromStackPanelTextBox(window, "QueryTypeInfoTextBox");
        }

        private void ClearStackPanelActionsView(Window window)
        {
            ClearTextFromStackPanelTextBox(window, "ActionNameTextBox");
            ClearTextFromStackPanelTextBox(window, "ActionInfoTextBox");
            ClearTextFromStackPanelTextBox(window, "ActionNumberTextBox");
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