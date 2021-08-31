using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Media;
using DocumentVisor.Annotations;
using DocumentVisor.Model;
using DocumentVisor.View;
using static DocumentVisor.MainWindow;

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
            set
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
		#endregion

		#region Persons

		private List<Person> _allPersons = DataWorker.GetAllPersons();


		public static Person SelectedPerson { get; set; }

		public List<Person> AllPersons
		{
			get => _allPersons;
			set
			{
				_allPersons = value;
				OnPropertyChanged(nameof(AllPersons));
			}
		}



		#region Fields

		public static string PersonName { get; set; }
		public static string PersonInfo { get; set; }
		public static string PersonPhone { get; set; }
		public static PersonType PersonType { get; set; }



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
					if(PersonName == null || PersonName.Replace(" ", "").Length == 0) SetRedBlockControl(wnd, "PersonNameTextBox");
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

		#region Updates
		private void UpdateAllDataView()
		{
			UpdateAllPersonsView();
			UpdateAllPersonTypesView();
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
