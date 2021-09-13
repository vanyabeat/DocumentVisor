using System;
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

		//public static DataManageVm Instance { get; } = new DataManageVm();
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
            ClearTextFromStackPanel(window, "PersonTypeNameTextBox");
            ClearTextFromStackPanel(window, "PersonTypeInfoTextBox");
        }

        private RelayCommand _editPersonType;
        public RelayCommand EditUser
        {
            get
            {
                return _editPersonType ?? new RelayCommand(obj =>
                    {
                        Window window = obj as Window;
                        string resultStr = "Не выбран сотрудник";
                        string noPositionStr = "Не выбрана новая должность";
                        if (SelectedPersonType != null)
                        {

                                resultStr = DataWorker.EditPersonType(SelectedPersonType, PersonTypeName, PersonTypeInfo);

                                UpdateAllDataView();
                                SetNullValuesToProperties();
                                ShowMessageToUser(resultStr);
                                window.Close();
                                
                        }
                        else ShowMessageToUser(resultStr);

                    }
                );
            }
        }
		#endregion

		#region Persons
        private List<Person> _allPersons = DataWorker.GetAllPersons();
        public static Person SelectedPerson { get; set; }
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
                        ClearStackPanelPersonTypesView(wnd);
                    }
                });
            }
        }
        #endregion
        #region Commands

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
                        result = DataWorker.CreatePerson(PersonName, PersonInfo, PersonPhone, PersonType);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ShowMessageToUser(result);
                    }
                }
                );
            }
        }




        #endregion
        #region Fields

        public static string PersonName { get; set; }
        public static string PersonInfo { get; set; }
        public static string PersonPhone { get; set; }
        public static PersonType PersonType { get; set; }

        private void UpdateAllPrivacyView()
        {
            AllPrivacies = DataWorker.GetAllPrivacies();
            AllPrivaciesView.ItemsSource = null;
            AllPrivaciesView.Items.Clear();
            AllPrivaciesView.ItemsSource = AllPersons;
            AllPrivaciesView.Items.Refresh();
        }

        #endregion
        #region Updates
        private void UpdateAllDataView()
        {
            UpdateAllPersonsView();
            UpdateAllPersonTypesView();
            UpdateAllPrivacyView();
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
                            break;
                        case "PersonTypesTab" when SelectedPersonType != null:
                            result = DataWorker.DeletePersonType(SelectedPersonType);
                            UpdateAllDataView();
                            ClearStackPanelPersonTypesView(wnd);
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
        }

        #endregion
        #region Utils
        private void SetRedBlockControl(Window window, string blockName)
        {
            var block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Crimson;
        }

        private void ClearTextFromStackPanel(Window window, string blockName)
        {
            var block = window.FindName(blockName) as TextBox;
            block.Clear();
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
            EditPersonTypeView editDepartmentWindow = new EditPersonTypeView(personType);
            SetCenterPositionAndOpen(editDepartmentWindow);
        }
        private RelayCommand _openEditItemWnd;
        public RelayCommand OpenEditItemWnd
        {
            get
            {
                return _openEditItemWnd ?? new RelayCommand(obj =>
                    {
                        //если сотрудник
                        if (SelectedTabItem.Name == "PersonTypesTab" && SelectedPersonType != null)
                        {
                            OpenEditPersonTypeViewMethod(SelectedPersonType);
                        }
                        ////если позиция
                        //if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
                        //{
                        //    OpenEditPositionWindowMethod(SelectedPosition);
                        //}
                        ////если отдел
                        //if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                        //{
                        //    OpenEditDepartmentWindowMethod(SelectedDepartment);
                        //}
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
