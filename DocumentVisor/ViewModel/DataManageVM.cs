using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DocumentVisor.Infrastructure;
using DocumentVisor.Model;
using DocumentVisor.View;
using Microsoft.Win32;
using Newtonsoft.Json;
using Spire.Xls;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Converter;
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

        private ObservableCollection<PersonType> _allPersonTypes = DataWorker.GetAllPersonTypes();

        public ObservableCollection<PersonType> AllPersonTypes
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

        private readonly RelayCommand<object> _addNewPersonType = null;

        public RelayCommand<object> AddNewPersonType
        {
            get
            {
                return _addNewPersonType ?? new RelayCommand<object>(obj =>
                {
                    var wnd = obj as Window;
                    if (PersonTypeName == null || PersonTypeName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "PersonTypeNameTextBox");
                        UpdateAllDataView();
                    }
                    else
                    {
                        DataWorker.CreatePersonType(PersonTypeName, PersonTypeInfo);
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


        private readonly RelayCommand<object> _editPersonType = null;

        public RelayCommand<object> EditPersonType
        {
            get
            {
                return _editPersonType ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _addNewPerson = null;

        public RelayCommand<object> AddNewPerson
        {
            get
            {
                return _addNewPerson ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _editPerson = null;

        public RelayCommand<object> EditPerson
        {
            get
            {
                return _editPerson ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _addNewPrivacy = null;

        public RelayCommand<object> AddNewPrivacy
        {
            get
            {
                return _addNewPrivacy ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _editPrivacy = null;

        public RelayCommand<object> EditPrivacy
        {
            get
            {
                return _editPrivacy ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _addNewDivision = null;

        public RelayCommand<object> AddNewDivision
        {
            get
            {
                return _addNewDivision ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _editDivision = null;

        public RelayCommand<object> EditDivision
        {
            get
            {
                return _editDivision ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _addNewTheme = null;

        public RelayCommand<object> AddNewTheme
        {
            get
            {
                return _addNewTheme ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _editTheme = null;

        public RelayCommand<object> EditTheme
        {
            get
            {
                return _editTheme ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _addNewArticle = null;

        public RelayCommand<object> AddNewArticle
        {
            get
            {
                return _addNewArticle ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _editArticle = null;

        public RelayCommand<object> EditArticle
        {
            get
            {
                return _editArticle ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _addNewQueryType = null;

        public RelayCommand<object> AddNewQueryType
        {
            get
            {
                return _addNewQueryType ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _editQueryType = null;

        public RelayCommand<object> EditQueryType
        {
            get
            {
                return _editQueryType ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _addNewAction = null;

        public RelayCommand<object> AddNewAction
        {
            get
            {
                return _addNewAction ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _editAction = null;

        public RelayCommand<object> EditAction
        {
            get
            {
                return _editAction ?? new RelayCommand<object>(obj =>
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

        public static Query SelectedQuery { get; set; }
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
            set
            {
                _queryExecutorPersons = value;
                OnPropertyChanged(nameof(QueryExecutorPersons));
            }
        }

        private SortedSet<Theme> _queryThemes;

        public SortedSet<Theme> QueryThemes
        {
            get => _queryThemes;
            set
            {
                _queryThemes = value;
                OnPropertyChanged(nameof(QueryThemes));
            }
        }

        private SortedSet<Article> _queryArticles;

        public SortedSet<Article> QueryArticles
        {
            get => _queryArticles;
            set
            {
                _queryArticles = value;
                OnPropertyChanged(nameof(QueryArticles));
            }
        }

        private SortedSet<Action> _queryActions;

        public SortedSet<Action> QueryActions
        {
            get => _queryActions;
            set
            {
                _queryActions = value;
                OnPropertyChanged(nameof(QueryActions));
            }
        }

        private readonly RelayCommand<object> _createGuid = null;

        public static DateTime QueryReportBeginDateTime { get; set; }

        public static DateTime QueryReportEndDateTime { get; set; }

        public RelayCommand<object> CreateGuid
        {
            get
            {
                return _createGuid ?? new RelayCommand<object>(obj =>
                    {
                        var window = obj as Window;
                        QueryGuid = GenerateRandomGuid();
                        if (window?.FindName("QueryGuidTextBox") is TextBox textBox) textBox.Text = QueryGuid;
                    }
                );
            }
        }


        private readonly RelayCommand<object> _addNewQuery = null;

        public RelayCommand<object> AddNewQuery
        {
            get
            {
                return _addNewQuery ?? new RelayCommand<object>(obj =>
                    {
                        var wnd = obj as Window;

                        if (QueryName == null || QueryName.Replace(" ", "").Length == 0 || QueryType == null || QueryDivision == null || QueryGuid == null || QueryPrivacy == null || QuerySignPerson == null)
                        {
                            // ShowMessageToUser(Dictionary["ArticleNameNeedToSelect"].ToString());
                            SetRedBlockControl(wnd, "QueryNameTextBox");
                            SetRedBlockControl(wnd, "QueryGuidTextBox");
                            SetRedBlockControlComboBox(wnd, "QueryPrivacyComboBox");
                            SetRedBlockControlComboBox(wnd, "QueryDivisionComboBox");
                            SetRedBlockControlComboBox(wnd, "QueryQueryTypeComboBox");
                            SetRedBlockControlComboBox(wnd, "QuerySignPersonComboBox");
                        }
                        else
                        {
                            var result = DataWorker.CreateQuery(QueryName, QueryInfo, QueryGuid, QueryPrivacy,
                                QueryDivision, QuerySignPerson, QueryType, QueryOuterSecretaryDateTime,
                                QueryOuterSecretaryNumber,
                                QueryInnerSecretaryDateTime, QueryInnerSecretaryNumber, QueryCentralSecretaryDateTime,
                                QueryCentralSecretaryNumber, QueryHasCd, QueryVarious, QueryEmpty, false);
                            if (result > 0)
                            {
                                if (QueryExecutorPersons != null)
                                    foreach (var per in QueryExecutorPersons)
                                        DataWorker.QueryPersonLink(result, per.Id);

                                if (QueryArticles != null)
                                    foreach (var art in QueryArticles)
                                        DataWorker.QueryArticleLink(result, art.Id);

                                if (QueryActions != null)
                                    foreach (var action in QueryActions)
                                        DataWorker.QueryActionLink(result, action.Id);

                                if (QueryThemes != null)
                                    foreach (var theme in QueryThemes)
                                        DataWorker.QueryThemeLink(result, theme.Id);
                            }

                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            wnd.Close();
                        }
                    }
                );
            }
        }


        private readonly RelayCommand<object> _editQuery = null;

        public RelayCommand<object> EditQuery
        {
            get
            {
                return _editQuery ?? new RelayCommand<object>(obj =>
                {
                    var wnd = obj as Window;

                    if (SelectedQuery == null)
                    {
                        ShowMessageToUser(Dictionary["ExecutorPersonNeedToSelect"].ToString());
                    }
                    else
                    {
                        var result = DataWorker.EditQuery(SelectedQuery, QueryName, QueryInfo, QueryGuid,
                            QueryPrivacy, QueryDivision, QuerySignPerson, QueryType, QueryOuterSecretaryDateTime,
                            QueryOuterSecretaryNumber, QueryInnerSecretaryDateTime, QueryInnerSecretaryNumber,
                            QueryCentralSecretaryDateTime, QueryCentralSecretaryNumber, QueryHasCd, QueryVarious,
                            QueryEmpty);
                        result = DataWorker.EditQueryLinkedData(SelectedQuery, QueryExecutorPersons, QueryArticles,
                            QueryThemes, QueryActions);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                });
            }
        }

        private readonly RelayCommand<object> _addExecutorPerson = null;

        public RelayCommand<object> AddExecutorPerson
        {
            get
            {
                return _addExecutorPerson ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _addEditExecutorPerson = null;

        public RelayCommand<object> AddEditExecutorPerson
        {
            get
            {
                return _addEditExecutorPerson ?? new RelayCommand<object>(obj =>
                    {
                        var wnd = obj as EditQueryView;

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


        private readonly RelayCommand<object> _addQueryTheme = null;

        public RelayCommand<object> AddQueryTheme
        {
            get
            {
                return _addQueryTheme ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _addEditQueryTheme = null;

        public RelayCommand<object> AddEditQueryTheme
        {
            get
            {
                return _addEditQueryTheme ?? new RelayCommand<object>(obj =>
                    {
                        var wnd = obj as EditQueryView;

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

        private readonly RelayCommand<object> _deleteEditQueryTheme = null;

        public RelayCommand<object> DeleteEditQueryTheme
        {
            get
            {
                return _deleteEditQueryTheme ?? new RelayCommand<object>(obj =>
                    {
                        var wnd = obj as EditQueryView;

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

        private readonly RelayCommand<object> _deleteExecutorPerson = null;

        public RelayCommand<object> DeleteExecutorPerson
        {
            get
            {
                return _deleteExecutorPerson ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _deleteEditExecutorPerson = null;

        public RelayCommand<object> DeleteEditExecutorPerson
        {
            get
            {
                return _deleteEditExecutorPerson ?? new RelayCommand<object>(obj =>
                    {
                        var wnd = obj as EditQueryView;

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


        private readonly RelayCommand<object> _deleteQueryTheme = null;

        public RelayCommand<object> DeleteQueryTheme
        {
            get
            {
                return _deleteQueryTheme ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _addQueryArticle = null;

        public RelayCommand<object> AddQueryArticle
        {
            get
            {
                return _addQueryArticle ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _addEditQueryArticle = null;

        public RelayCommand<object> AddEditQueryArticle
        {
            get
            {
                return _addEditQueryArticle ?? new RelayCommand<object>(obj =>
                    {
                        var wnd = obj as EditQueryView;

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

        private readonly RelayCommand<object> _deleteQueryArticle = null;

        public RelayCommand<object> DeleteQueryArticle
        {
            get
            {
                return _deleteQueryArticle ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _deleteEditQueryArticle = null;

        public RelayCommand<object> DeleteEditQueryArticle
        {
            get
            {
                return _deleteEditQueryArticle ?? new RelayCommand<object>(obj =>
                    {
                        var wnd = obj as EditQueryView;

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


        private readonly RelayCommand<object> _addQueryAction = null;

        public RelayCommand<object> AddQueryAction
        {
            get
            {
                return _addQueryAction ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _addEditQueryAction = null;

        public RelayCommand<object> AddEditQueryAction
        {
            get
            {
                return _addEditQueryAction ?? new RelayCommand<object>(obj =>
                    {
                        var wnd = obj as EditQueryView;

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

        private readonly RelayCommand<object> _deleteQueryAction = null;

        public RelayCommand<object> DeleteQueryAction
        {
            get
            {
                return _deleteQueryAction ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _deleteEditQueryAction = null;

        public RelayCommand<object> DeleteEditQueryAction
        {
            get
            {
                return _deleteEditQueryAction ?? new RelayCommand<object>(obj =>
                    {
                        var wnd = obj as EditQueryView;

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

        private static Task ExportToExcelQueries(SfDataGrid dataGrid)
        {
            var options = new ExcelExportingOptions
            {
                AllowOutlining = true
            };
            var excelEngine = dataGrid.ExportToExcel(dataGrid.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text File (*.xlsx)|*.xlsx|Show All Files (*.*)|*.*",
                FileName = "DocumentVisorOut",
                Title = "Save As"
            };
            if (saveFileDialog.ShowDialog() != null)
            {
                workBook.SaveAs(saveFileDialog.FileName);
                var workbook = new Workbook();
                workbook.LoadFromFile(saveFileDialog.FileName);
                workbook.Worksheets[^1].Remove();
                workbook.SaveToFile(saveFileDialog.FileName, ExcelVersion.Version2010);
            }

            ;
            return Task.CompletedTask;
        }

        private readonly AsyncRelayCommand<object> _exportToExcel = null;

        public AsyncRelayCommand<object> ExportToExcel
        {
            get
            {
                return _exportToExcel ?? new AsyncRelayCommand<object>(async obj =>
                    {
                        if (!(obj is Window wnd)) return;
                        var dataGrid = wnd.FindName("QueriesDataGrid") as SfDataGrid;
                        Mouse.OverrideCursor = Cursors.Wait;
                        await ExportToExcelQueries(dataGrid);
                        Mouse.OverrideCursor = Cursors.Arrow;
                    }
                );
            }
        }


        public static async Task WriteToFileAsync(string filename, string text)
        {
            await File.WriteAllTextAsync(filename, text);
        }

        private string GenerateTable(SortedDictionary<Person, Tuple<int, int>> sortedDictionary)
        {
            var result = "";

            foreach (var pair in sortedDictionary)
            {
                var pattern =
                    $"<tr style=\"height: 18px;\"><td style=\"width: 25%; height: 18px;\">&nbsp;{pair.Key.Name}</td><td style=\"width: 25%; height: 18px; text-align: center;\">{pair.Value.Item1} ({pair.Value.Item2})</td><td style=\"width: 25%; height: 18px; text-align: center;\">&nbsp;</td></tr>";
                result += pattern;
            }

            return result;
        }
        private readonly AsyncRelayCommand<object> _generateHtmlReport2 = null;

        private static string GenerateLiStrings(SortedDictionary<string, SortedSet<string>> data)
        {
            string result = "";

            foreach (var d in data)
            {
                result += $"<li style=\"text-align: left;\">{d.Key} ";
                if (d.Value.Count != 0)
                {
                    result += $"({string.Join(", ", d.Value)})";
                }

                result += ";</li>";
            }
            return result;
        }
        public AsyncRelayCommand<object> GenerateHtmlReport2
        {
            get
            {
                return _generateHtmlReport2 ?? new AsyncRelayCommand<object>(async obj =>
                    {
                        var wnd = obj as MainWindow;

                        if (QueryReportBeginDateTime == null || QueryReportEndDateTime == null)
                        {
                            ShowMessageToUser(Dictionary["QueryActionNeedToSelect"].ToString());
                        }
                        else
                        {
                            var dicData =
                                DataWorker.GetReportByDivisions(QueryReportBeginDateTime, QueryReportEndDateTime);

                            var result = $@"
<!DOCTYPE html>
<html><body><p style=""text-align: center;""><span style=""text-decoration: underline;""><strong>Отчетный период с {QueryReportBeginDateTime:dd.MM.yyyy} по {QueryReportEndDateTime:dd.MM.yyyy}</strong></span></p><p style=""text-align: center;"">Выполнено {dicData.Item1} КРМ в интересах:</p><ul>
{GenerateLiStrings(dicData.Item2)}
</ul>
</body>
</html>
";
                            var saveFileDialog = new SaveFileDialog
                            {
                                Filter = "Text File (*.html)|*.htm|Show All Files (*.*)|*.*",
                                FileName = "ReportDate",
                                Title = "Save As"
                            };
                            if (saveFileDialog.ShowDialog() != null)
                            {
                                await WriteToFileAsync(saveFileDialog.FileName, result);
                                SetNullValuesToProperties();
                                wnd.QueryBeginSortDatetime.SelectedDate = DateTime.Now;
                                wnd.QueryEndSortDatetime.SelectedDate = DateTime.Now + TimeSpan.FromDays(1);
                            }
                                

                        }
                    }
                );
            }
        }

        private readonly AsyncRelayCommand<object> _generateHtmlReport = null;

        public AsyncRelayCommand<object> GenerateHtmlReport
        {
            get
            {
                return _generateHtmlReport ?? new AsyncRelayCommand<object>(async obj =>
                    {
                        var wnd = obj as MainWindow;

                        if (QueryReportBeginDateTime == null || QueryReportEndDateTime == null)
                        {
                            ShowMessageToUser(Dictionary["QueryActionNeedToSelect"].ToString());
                        }
                        else
                        {
                            var queries =
                                DataWorker.GetAllQueriesByDate(QueryReportBeginDateTime, QueryReportEndDateTime);
                            var ul = string.Join("", queries.Where(x => x.IsComplete == 1).Select(x => x.ToString()).ToArray());
                            var personData =
                                DataWorker.GetAllQueriesStatisticsByPerson(queries);
                            var queriesCompleted =
                                DataWorker.GetAllQueriesByDateCompleted(QueryReportBeginDateTime,
                                    QueryReportEndDateTime);
                            var result = $@"
<!DOCTYPE html>
<html>
<body>
<h2 style=""text-align: center;"">Отчетный период с {QueryReportBeginDateTime:dd.MM.yyyy} по {QueryReportEndDateTime:dd.MM.yyyy}</h2>
<table style=""border-collapse: collapse; width: 100%;"" border=""1"">
   <tbody>
      <tr>
         <td style=""text-align: center;"">п/н</td>
         <td style=""text-align: center;"">Проведено КРМ</td>
         <td style=""text-align: center;"">Количество</td>
         <td style=""text-align: center;"">Примечание</td>
      </tr>
      <tr>
         <td style=""text-align: center;"">1</td>
         <td style=""text-align: center;"">&nbsp;Поступило ш/т и запросов</td>
         <td style=""text-align: center;"">&nbsp;{queries.Count}</td>
         <td style=""text-align: center;"">&nbsp;</td>
      </tr>
      <tr>
         <td style=""text-align: center;"">2</td>
         <td style=""text-align: center;"">&nbsp;Выполнено ш/т и запросов</td>
         <td style=""text-align: center;"">&nbsp;{queriesCompleted}</td>
         <td style=""text-align: center;"">&nbsp;</td>
      </tr>
      <tr>
         <td style=""text-align: center;"">3</td>
         <td style=""text-align: center;"">&nbsp;Очередь запросов</td>
         <td style=""text-align: center;"">&nbsp;{DataWorker.GetAllQueriesNonComplete()}</td>
         <td style=""text-align: center;"">&nbsp;</td>
      </tr>
   </tbody>
</table>
&nbsp;
&nbsp;
&nbsp;
&nbsp;
<table style=""border-collapse: collapse; width: 100%; height: 48px;"" border=""1"">
<tbody>
<tr style=""height: 12px;"">
<td style=""width: 25%; height: 12px; text-align: center;"">Сотрудники</td>
<td style=""width: 25%; height: 12px; text-align: center;"">&nbsp;Кол-во запросов</td>
<td style=""width: 25%; height: 12px; text-align: center;"">Примечание</td>
</tr>
{GenerateTable(personData)}
<tr style=""height: 18px;"">
<td style=""width: 25%; height: 18px; text-align: center;"" colspan=""2"">Итого:</td>
<td style=""width: 25%; height: 18px;"">&nbsp;{queriesCompleted}</td>
</tr>
</tbody>
</table>

<ul style=""list-style-type: circle;"">
  {ul}
</ul>
</body>
</html>
";
                            var saveFileDialog = new SaveFileDialog
                            {
                                Filter = "Text File (*.html)|*.htm|Show All Files (*.*)|*.*",
                                FileName = "ReportDate",
                                Title = "Save As"
                            };
                            if (saveFileDialog.ShowDialog() != null)
                            {
                                await WriteToFileAsync(saveFileDialog.FileName, result);
                                SetNullValuesToProperties();
                                wnd.QueryBeginSortDatetime.SelectedDate = DateTime.Now;
                                wnd.QueryEndSortDatetime.SelectedDate = DateTime.Now + TimeSpan.FromDays(1);
                            }
                        }
                    }
                );
            }
        }

        #endregion

        #region IdentifierTypes

        private ObservableCollection<IdentifierType> _allIdentifierTypes = DataWorker.GetAllIdentifierTypes();

        public ObservableCollection<IdentifierType> AllIdentifierTypes
        {
            get => _allIdentifierTypes;
            private set
            {
                _allIdentifierTypes = value;
                OnPropertyChanged(nameof(AllPersonTypes));
            }
        }

        public static IdentifierType SelectedIdentifierType { get; set; }
        public static string IdentifierTypeName { get; set; }
        public static string IdentifierTypeInfo { get; set; }

        private readonly RelayCommand<object> _addNewIdentifierType = null;

        public RelayCommand<object> AddNewIdentifierType
        {
            get
            {
                return _addNewIdentifierType ?? new RelayCommand<object>(obj =>
                {
                    var wnd = obj as Window;
                    if (IdentifierTypeName == null || IdentifierTypeName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "IdentifierTypeNameTextBox");
                        UpdateAllDataView();
                    }
                    else
                    {
                        DataWorker.CreateIdentifierType(IdentifierTypeName, IdentifierTypeInfo);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ClearStackPanelIdentifierTypesView(wnd);
                    }
                });
            }
        }

        private void UpdateAllIdentifierTypeView()
        {
            AllIdentifierTypes = DataWorker.GetAllIdentifierTypes();
            AllIdentifierTypesView.ItemsSource = null;
            AllIdentifierTypesView.Items.Clear();
            AllIdentifierTypesView.ItemsSource = AllIdentifierTypes;
            AllIdentifierTypesView.Items.Refresh();
        }


        private readonly RelayCommand<object> _editIdentifierType = null;

        public RelayCommand<object> EditIdentifierType
        {
            get
            {
                return _editIdentifierType ?? new RelayCommand<object>(obj =>
                    {
                        var window = obj as Window;
                        if (SelectedIdentifierType != null)
                        {
                            var result = DataWorker.EditIdentifierType(SelectedIdentifierType, IdentifierTypeName,
                                IdentifierTypeInfo);

                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(result);
                            window.Close();
                        }
                    }
                );
            }
        }


        private readonly AsyncRelayCommand<object> _generateIdentifierTypesJson = null;

        public AsyncRelayCommand<object> GenerateIdentifierTypesJson
        {
            get
            {
                return _generateIdentifierTypesJson ?? new AsyncRelayCommand<object>(async obj =>
                    {
                        var wnd = obj as Window;
                        var export = new Dictionary<string, object>();
                        export.Add("Divisions", DataWorker.GetAllDivisions());
                        export.Add("IdentifierTypes", DataWorker.GetAllIdentifierTypes());
                        var result = DataWorker.GetJsonString(export);
                        var saveFileDialog = new SaveFileDialog
                        {
                            Filter = "Text File (*.json)|*.json|Show All Files (*.*)|*.*",
                            FileName = "data_types",
                            Title = "Save As"
                        };
                        if (saveFileDialog.ShowDialog() != null)
                            await WriteToFileAsync(saveFileDialog.FileName, result);
                        // workBook.SaveAs(saveFileDialog.FileName);
                        ;
                    }
                );
            }
        }

        #endregion

        #region Updates

        private void UpdateAllDataView()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            UpdateAllPersonsView();
            UpdateAllPersonTypesView();
            UpdateAllPrivacyView();
            UpdateAllThemeView();
            UpdateAllDivisionView();
            UpdateAllQueryTypesView();
            UpdateAllActionView();
            UpdateAllArticleView();
            UpdateAllQueriesView();
            UpdateAllIdentifierTypeView();
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        #endregion

        #region Deletes

        private readonly RelayCommand<object> _deleteItem = null;

        public RelayCommand<object> DeleteItem
        {
            get
            {
                return _deleteItem ?? new RelayCommand<object>(obj =>
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
                        case "IdentifierTypesTab" when SelectedIdentifierType != null:
                            result = DataWorker.DeleteIdentifierType(SelectedIdentifierType);
                            UpdateAllDataView();
                            ClearStackPanelIdentifierTypesView(wnd);
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
            // IdentifierType
            IdentifierTypeName = null;
            IdentifierTypeInfo = null;
            // Person
            PersonName = null;
            PersonInfo = null;
            PersonPhone = null;
            PersonType = null;
            PersonRank = null;
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
            QueryThemes = null;
            QueryReportBeginDateTime = DateTime.Now;
            QueryReportEndDateTime = DateTime.Now + TimeSpan.FromDays(1);
        }

        #endregion

        #region Utils

        private string GenerateRandomGuid()
        {
            var time = DateTime.Now;
            var guid = new Random((int)time.Ticks).Next().ToString()?[..5];
            return $"{time.Day}{time.Month}{time.Year.ToString().Substring(1, 3)}_{guid}";
        }

        private void SetRedBlockControl(Window window, string blockName)
        {
            var block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Crimson;
        }

        private void SetRedBlockControlComboBox(Window window, string blockName)
        {
            var block = window.FindName(blockName) as ComboBox;
            block.BorderBrush = Brushes.Crimson;
            block.Background = Brushes.Crimson;
        }

        private void ClearTextFromStackPanelTextBox(Window window, string blockName)
        {
            var block = window.FindName(blockName) as TextBox;
            block.Clear();
        }

        private void ClearTextFromStackPanelComboBox(Window window, string blockName)
        {
            var block = window.FindName(blockName) as ComboBox;
            if (block != null) block.SelectedItem = null;
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
            // var index = DataWorker.GetAllPersonTypes().FindIndex(a => a.Id == person.TypeId);

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

        private void OpenEditQueryMethod(Query query)
        {
            var wnd = new EditQueryView(query);
            SetCenterPositionAndOpen(wnd);
        }

        private void OpenEditIdentifierTypeMethod(IdentifierType selectedIdentifierType)
        {
            var wnd = new EditIdentifierTypeView(selectedIdentifierType);
            SetCenterPositionAndOpen(wnd);
        }

        private readonly RelayCommand<object> _openAddQueryWnd = null;

        public RelayCommand<object> OpenAddQueryWnd
        {
            get
            {
                return _openAddQueryWnd ?? new RelayCommand<object>(obj =>
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

        private readonly RelayCommand<object> _openEditItemWnd = null;

        public RelayCommand<object> OpenEditItemWnd
        {
            get
            {
                return _openEditItemWnd ?? new RelayCommand<object>(obj =>
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
                            case "QueriesTab" when SelectedQuery != null:
                                OpenEditQueryMethod(SelectedQuery);
                                return;
                            case "IdentifierTypesTab" when SelectedIdentifierType != null:
                                OpenEditIdentifierTypeMethod(SelectedIdentifierType);
                                return;
                            default:
                                ShowMessageToUser(Dictionary["PleaseSelectNeedleItem"].ToString());
                                return;
                        }
                    }
                );
            }
        }

        private readonly AsyncRelayCommand<object> _importJson = null;

        public AsyncRelayCommand<object> ImportJson
        {
            get
            {
                return _importJson ?? new AsyncRelayCommand<object>(obj =>
                    {
                        var listErrors = new List<string>();

                        var openPath = string.Empty;
                        var openFileDialog = new OpenFileDialog();
                        if (openFileDialog.ShowDialog() == true) openPath = openFileDialog.FileName;
                        var importList =
                            JsonConvert.DeserializeObject<List<ExecutorRecord>>(File.ReadAllText(openPath));
                        if (importList == null) return Task.CompletedTask;
                        foreach (var data in importList)
                        {
                            if (DataWorker.EditQueryImport(data.Guid, data.Info, data.HasCd, data.IsEmpty,
                                    data.IdentifiersJson,
                                    data.OutputDivisionId, data.OutputNumber, data.OutputNumberDate, data.BlobData) ==
                                -1)
                            {
                                listErrors.Add(data.Guid);
                            }
                            else
                            {
                                var identifiers = JsonConvert.DeserializeObject<List<Identifier>>(data.IdentifiersJson);
                                var guid = data.Guid;
                                if (identifiers != null)
                                    foreach (var va in identifiers)
                                    {
                                        var id = DataWorker.CreateIdentifier(va.Content, va.IdentifierTypeId);
                                        if (id != -1)
                                        {
                                            DataWorker.QueryIdentifierLink(DataWorker.GetQueryByGuid(guid), id);
                                        }
                                    }
                            }

                        }


                        if (listErrors.Count > 0)
                        {
                            MessageBox.Show($"{string.Join("\n", listErrors.ToArray())}", $"{Dictionary["ErrorListGuid"]}");
                        }
                        UpdateAllDataView();
                        MessageBox.Show(Dictionary["Complete"].ToString());
                        return Task.CompletedTask;
                    }
                );
            }
        }

        private readonly AsyncRelayCommand<object> _backupDb = null;

        public AsyncRelayCommand<object> BackupDb
        {
            get
            {
                return _backupDb ?? new AsyncRelayCommand<object>(obj =>
                    {
                        var dialog = new SaveFileDialog
                        {
                            Filter = "Text Files(*.db)|*.sqlite3|All(*.*)|*"
                        };

                        if (dialog.ShowDialog() == true)
                            File.WriteAllBytes(dialog.FileName, File.ReadAllBytes("Supervisor.db"));

                        return Task.CompletedTask;
                    }
                );
            }
        }

        private readonly RelayCommand<object> _downloadDataCommand = null;

        public RelayCommand<object> DownloadDataCommand
        {
            get
            {
                return _downloadDataCommand ?? new RelayCommand<object>(obj =>
                    {
                        var dialog = new SaveFileDialog
                        {
                            Filter = "Text Files(*.zip)|*.zip|All(*.*)|*"
                        };

                        if (dialog.ShowDialog() == true)
                            File.WriteAllBytes(dialog.FileName, DataWorker.GetExecutorRecordData(SelectedQuery));
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

        private void ClearStackPanelIdentifierTypesView(Window window)
        {
            ClearTextFromStackPanelTextBox(window, "IdentifierTypeInfoTextBox");
            ClearTextFromStackPanelTextBox(window, "IdentifierTypeNameTextBox");
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