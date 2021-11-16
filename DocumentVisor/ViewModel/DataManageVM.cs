using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DocumentVisor.Model;
using DocumentVisor.View;
using static DocumentVisor.View.MainWindow;
using static System.Guid;
using Action = DocumentVisor.Model.Action;
using Type = DocumentVisor.Model.Type;

namespace DocumentVisor.ViewModel
{
    public class DataManageVm : INotifyPropertyChanged
    {
        private static readonly ResourceDictionary Dictionary = new ResourceDictionary
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

        private readonly RelayCommand _addNewPersonType = null;

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


        private readonly RelayCommand _editPersonType = null;

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

        private readonly RelayCommand _addNewPerson = null;

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

        private readonly RelayCommand _editPerson = null;

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

        private readonly RelayCommand _addNewPrivacy = null;

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

        private readonly RelayCommand _editPrivacy = null;

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

        private readonly RelayCommand _addNewDivision = null;

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

        private readonly RelayCommand _editDivision = null;

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

        private readonly RelayCommand _addNewTheme = null;

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

        private readonly RelayCommand _editTheme = null;

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

        private readonly RelayCommand _addNewArticle = null;

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

        private readonly RelayCommand _editArticle = null;

        public RelayCommand EditArticle
        {
            get
            {
                return _editArticle ?? new RelayCommand(obj =>
                    {
                        var window = obj as Window;
                        if (SelectedArticle != null)
                        {
                            var result = DataWorker.EditArticle(SelectedArticle, ArticleName, ArticleExtendedName,
                                ArticleInfo);

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

        #region Type

        private List<Type> _allQueryTypes = DataWorker.GetAllQueryTypes();

        public List<Type> AllQueryTypes
        {
            get => _allQueryTypes;
            private set
            {
                _allQueryTypes = value;
                OnPropertyChanged(nameof(AllQueryTypes));
            }
        }

        private readonly RelayCommand _addNewQueryType = null;

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
        public static Type SelectedType { get; set; }

        private void UpdateAllQueryTypesView()
        {
            AllQueryTypes = DataWorker.GetAllQueryTypes();
            AllQueryTypesView.ItemsSource = null;
            AllQueryTypesView.Items.Clear();
            AllQueryTypesView.ItemsSource = AllQueryTypes;
            AllQueryTypesView.Items.Refresh();
        }

        private readonly RelayCommand _editQueryType = null;

        public RelayCommand EditQueryType
        {
            get
            {
                return _editQueryType ?? new RelayCommand(obj =>
                    {
                        var window = obj as Window;
                        if (SelectedType != null)
                        {
                            var result = DataWorker.EditQueryType(SelectedType, QueryTypeName, QueryTypeInfo);

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

        private readonly RelayCommand _addNewAction = null;

        public RelayCommand AddNewAction
        {
            get
            {
                return _addNewAction ?? new RelayCommand(obj =>
                    {
                        var wnd = obj as Window;

                        if (ActionName == null || ActionName.Replace(" ", "").Length == 0)
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

        private readonly RelayCommand _editAction = null;

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
        public static string QueryInfo { get; set; }
        public static Privacy QueryPrivacy { get; set; }
        public static Division QueryDivision { get; set; }
        public static Person QuerySignPerson { get; set; }
        public static Type QueryType { get; set; }
        public static DateTime QueryOuterSecretaryDateTime { get; set; }
        public static string QueryOuterSecretaryNumber { get; set; }
        public static DateTime QueryInnerSecretaryDateTime { get; set; }
        public static string QueryInnerSecretaryNumber { get; set; }
        public static DateTime QueryCentralSecretaryDateTime { get; set; }
        public static string QueryCentralSecretaryNumber { get; set; }
        public static bool QueryHasCd { get; set; }
        public static bool QueryVarious { get; set; }
        public static bool QueryEmpty { get; set; }
        public static Person SelectedQueryExecutorPerson { get; set; }
        public static Person QueryCurrentExecutorPerson { get; set; }
        public static Theme SelectedQueryTheme { get; set; }
        public static Theme QueryCurrentTheme { get; set; }
        public static Article SelectedQueryArticle { get; set; }
        public static Article QueryCurrentArticle { get; set; }
        public static Action SelectedQueryAction { get; set; }
        public static Action QueryCurrentAction { get; set; }
        private SortedSet<Person> _queryExecutorPersons;

        public SortedSet<Person> QueryExecutorPersons
        {
            get => _queryExecutorPersons;
            private set
            {
                _queryExecutorPersons = value;
                OnPropertyChanged(nameof(QueryExecutorPersons));
            }
        }

        private SortedSet<Theme> _queryThemes;

        public SortedSet<Theme> QueryThemes
        {
            get => _queryThemes;
            private set
            {
                _queryThemes = value;
                OnPropertyChanged(nameof(QueryThemes));
            }
        }

        private SortedSet<Article> _queryArticles;

        public SortedSet<Article> QueryArticles
        {
            get => _queryArticles;
            private set
            {
                _queryArticles = value;
                OnPropertyChanged(nameof(QueryArticles));
            }
        }

        private SortedSet<Action> _queryActions;

        public SortedSet<Action> QueryActions
        {
            get => _queryActions;
            private set
            {
                _queryActions = value;
                OnPropertyChanged(nameof(QueryActions));
            }
        }

        private readonly RelayCommand _createGuid = null;

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


        private readonly RelayCommand _addNewQuery = null;

        public RelayCommand AddNewQuery
        {
            get
            {
                return _addNewQuery ?? new RelayCommand(obj =>
                    {
                        var wnd = obj as Window;

                        if (QueryName == null || QueryName.Replace(" ", "").Length == 0)
                        {
                            ShowMessageToUser(Dictionary["ArticleNameNeedToSelect"].ToString());
                        }
                        else
                        {
                            var result = DataWorker.CreateQuery(QueryName, QueryInfo, QueryGuid, QueryPrivacy,
                                QueryDivision, QuerySignPerson, QueryType, QueryOuterSecretaryDateTime,
                                QueryOuterSecretaryNumber,
                                QueryInnerSecretaryDateTime, QueryInnerSecretaryNumber, QueryCentralSecretaryDateTime,
                                QueryCentralSecretaryNumber, QueryHasCd, QueryVarious, QueryEmpty);
                            if (result > 0)
                            {
                                if (QueryExecutorPersons != null)
                                {
                                    foreach (var per in QueryExecutorPersons)
                                    {
                                        DataWorker.QueryPersonLink(result, per.Id);
                                    }
                                }

                                if (QueryArticles != null)
                                {
                                    foreach (var art in QueryArticles)
                                    {
                                        DataWorker.QueryArticleLink(result, art.Id);
                                    }
                                }

                                if (QueryActions != null)
                                {
                                    foreach (var action in QueryActions)
                                    {
                                        DataWorker.QueryActionLink(result, action.Id);
                                    }
                                }

                                if (QueryThemes != null)
                                {
                                    foreach (var theme in QueryThemes)
                                    {
                                        DataWorker.QueryThemeLink(result, theme.Id);
                                    }
                                }

                                    
                            }

                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            wnd.Close();
                        }
                    }
                );
            }
        }

        private readonly RelayCommand _addExecutorPerson = null;

        public RelayCommand AddExecutorPerson
        {
            get
            {
                return _addExecutorPerson ?? new RelayCommand(obj =>
                    {
                        var wnd = obj as AddQueryView;

                        if (QueryCurrentExecutorPerson == null)
                        {
                            ShowMessageToUser(Dictionary["ExecutorPersonNeedToSelect"].ToString());
                        }
                        else
                        {
                            var person = DataWorker.GetPersonById(QueryCurrentExecutorPerson.Id);
                            QueryExecutorPersons ??= new SortedSet<Person>();
                            QueryExecutorPersons.Add(person);
                            QueryCurrentExecutorPerson = null;
                            wnd.ExecutorPersonsDataGrid.Items.Refresh();
                            wnd.QueryExecutorComboBox.SelectedItem = null;
                        }
                    }
                );
            }
        }

        private readonly RelayCommand _addQueryTheme = null;

        public RelayCommand AddQueryTheme
        {
            get
            {
                return _addQueryTheme ?? new RelayCommand(obj =>
                    {
                        var wnd = obj as AddQueryView;

                        if (QueryCurrentTheme == null)
                        {
                            ShowMessageToUser(Dictionary["QueryThemeNeedToSelect"].ToString());
                        }
                        else
                        {
                            var theme = DataWorker.GetThemeById(QueryCurrentTheme.Id);
                            QueryThemes ??= new SortedSet<Theme>();
                            QueryThemes.Add(theme);
                            QueryCurrentTheme = null;
                            wnd.QueryThemesDataGrid.Items.Refresh();
                            wnd.QueryThemeComboBox.SelectedItem = null;
                        }
                    }
                );
            }
        }

        private readonly RelayCommand _deleteExecutorPerson = null;

        public RelayCommand DeleteExecutorPerson
        {
            get
            {
                return _deleteExecutorPerson ?? new RelayCommand(obj =>
                    {
                        var wnd = obj as AddQueryView;

                        if (SelectedQueryExecutorPerson == null)
                        {
                            ShowMessageToUser(Dictionary["ExecutorPersonNeedToSelect"].ToString());
                        }
                        else
                        {
                            var person = DataWorker.GetPersonById(SelectedQueryExecutorPerson.Id);
                            QueryExecutorPersons.Remove(person);
                            SelectedQueryExecutorPerson = null;
                            wnd.ExecutorPersonsDataGrid.Items.Refresh();
                            wnd.QueryExecutorComboBox.SelectedItem = null;
                        }
                    }
                );
            }
        }

        private readonly RelayCommand _deleteQueryTheme = null;

        public RelayCommand DeleteQueryTheme
        {
            get
            {
                return _deleteQueryTheme ?? new RelayCommand(obj =>
                    {
                        var wnd = obj as AddQueryView;

                        if (SelectedQueryTheme == null)
                        {
                            ShowMessageToUser(Dictionary["QueryThemeNeedToSelect"].ToString());
                        }
                        else
                        {
                            var person = DataWorker.GetThemeById(SelectedQueryTheme.Id);
                            QueryThemes.Remove(person);
                            SelectedQueryTheme = null;
                            wnd.QueryThemesDataGrid.Items.Refresh();
                            wnd.QueryThemeComboBox.SelectedItem = null;
                        }
                    }
                );
            }
        }

        private readonly RelayCommand _addQueryArticle = null;

        public RelayCommand AddQueryArticle
        {
            get
            {
                return _addQueryArticle ?? new RelayCommand(obj =>
                    {
                        var wnd = obj as AddQueryView;

                        if (QueryCurrentArticle == null)
                        {
                            ShowMessageToUser(Dictionary["QueryArticleNeedToSelect"].ToString());
                        }
                        else
                        {
                            var article = DataWorker.GetArticleById(QueryCurrentArticle.Id);
                            QueryArticles ??= new SortedSet<Article>();
                            QueryArticles.Add(article);
                            QueryCurrentArticle = null;
                            wnd.QueryArticlesDataGrid.Items.Refresh();
                            wnd.QueryArticleComboBox.SelectedItem = null;
                        }
                    }
                );
            }
        }

        private readonly RelayCommand _deleteQueryArticle = null;

        public RelayCommand DeleteQueryArticle
        {
            get
            {
                return _deleteQueryArticle ?? new RelayCommand(obj =>
                    {
                        var wnd = obj as AddQueryView;

                        if (SelectedQueryArticle == null)
                        {
                            ShowMessageToUser(Dictionary["QueryArticleNeedToSelect"].ToString());
                        }
                        else
                        {
                            var article = DataWorker.GetArticleById(SelectedQueryArticle.Id);
                            QueryArticles.Remove(article);
                            SelectedQueryArticle = null;
                            wnd.QueryArticlesDataGrid.Items.Refresh();
                            wnd.QueryArticleComboBox.SelectedItem = null;
                        }
                    }
                );
            }
        }


        private readonly RelayCommand _addQueryAction = null;

        public RelayCommand AddQueryAction
        {
            get
            {
                return _addQueryAction ?? new RelayCommand(obj =>
                    {
                        var wnd = obj as AddQueryView;

                        if (QueryCurrentAction == null)
                        {
                            ShowMessageToUser(Dictionary["QueryActionNeedToSelect"].ToString());
                        }
                        else
                        {
                            var action = DataWorker.GetActionById(QueryCurrentAction.Id);
                            QueryActions ??= new SortedSet<Action>();
                            QueryActions.Add(action);
                            QueryCurrentAction = null;
                            wnd.QueryActionDataGrid.Items.Refresh();
                            wnd.QueryActionComboBox.SelectedItem = null;
                        }
                    }
                );
            }
        }

        private readonly RelayCommand _deleteQueryAction = null;

        public RelayCommand DeleteQueryAction
        {
            get
            {
                return _deleteQueryAction ?? new RelayCommand(obj =>
                    {
                        var wnd = obj as AddQueryView;

                        if (SelectedQueryAction == null)
                        {
                            ShowMessageToUser(Dictionary["QueryActionNeedToSelect"].ToString());
                        }
                        else
                        {
                            var article = DataWorker.GetActionById(SelectedQueryAction.Id);
                            QueryActions.Remove(article);
                            SelectedQueryAction = null;
                            wnd.QueryActionDataGrid.Items.Refresh();
                            wnd.QueryActionComboBox.SelectedItem = null;
                        }
                    }
                );
            }
        }

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

        public void UpdateAllQueriesView()
        {
            AllQueries = DataWorker.GetAllQueries();
            AllQueriesView.ItemsSource = AllQueries;
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
            UpdateAllQueryTypesView();
            UpdateAllActionView();
            UpdateAllArticleView();
            UpdateAllQueriesView();
        }

        #endregion

        #region Deletes

        private readonly RelayCommand _deleteItem = null;

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
                        case "QueryTypesTab" when SelectedType != null:
                            result = DataWorker.DeleteQueryType(SelectedType);
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
            // Type
            QueryTypeInfo = null;
            QueryTypeName = null;
            // Actions
            ActionInfo = null;
            ActionName = null;
            ActionNumber = null;
            // Query
            QueryName = null;
            QueryGuid = null;
            QueryInfo = null;
            QueryPrivacy = null;
            QueryDivision = null;
            QuerySignPerson = null;
            QueryType = null;
            QueryOuterSecretaryDateTime = DateTime.Now;
            QueryOuterSecretaryNumber = null;
            QueryInnerSecretaryDateTime = DateTime.Now;
            QueryInnerSecretaryNumber = null;
            QueryCentralSecretaryDateTime = DateTime.Now;
            QueryCentralSecretaryNumber = null;
            QueryEmpty = false;
            QueryVarious = false;
            QueryHasCd = false;
            QueryExecutorPersons = null;
            QueryActions = null;
            QueryArticles = null;
            QueryExecutorPersons = null;
            QueryThemes = null;
        }

        #endregion

        #region Utils

        private string GenerateRandomGuid()
        {
            var time = DateTime.Now;
            var guid = NewGuid().ToString().Substring(0, 7);
            return $"{time.Day}{time.Month}{time.Year.ToString().Substring(1, 3)}_{guid}";
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

        private void OpenEditQueryTypeViewMethod(Type type)
        {
            var wnd = new EditQueryTypeView(type);
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

        private readonly RelayCommand _openAddQueryWnd = null;

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

        private readonly RelayCommand _openEditItemWnd = null;

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
                            case "QueryTypesTab" when SelectedType != null:
                                OpenEditQueryTypeViewMethod(SelectedType);
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