using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using DocumentVisor.Annotations;
using DocumentVisor.Model;
using DocumentVisor.View;

namespace DocumentVisor.ViewModel
{
    public class DataManageVm : INotifyPropertyChanged
    {
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
                        string result;
                        var control = obj as UserControl;

                        if (PersonName == null || PersonName.Replace(" ", "").Length == 0)
                        {
                            SetRedBlockControl(control, "NameBlock");
                        }
                        else
                        {
                            result = DataWorker.CreatePerson(PersonName, PersonInfo, PersonPhone, PersonType);
                            UpdateAllPersonsView((PersonsView) control);
                            SetNullValuesToProperties();
                            SetNullPersonTextBoxes(control as PersonsView);
                        }

                    }
                );
            }
        }

        private RelayCommand _deletePerson;

        public RelayCommand DeletePerson
        {
            get
            {

                return _deletePerson ?? new RelayCommand(obj =>
                {
                    var control = obj as UserControl;
                    if (SelectedPerson == null) return;
                    DataWorker.DeletePerson(SelectedPerson);
                    SetNullValuesToProperties();
                    UpdateAllPersonsView(control as PersonsView);
                });
            }
        }

        #endregion

        #region Updates

        private void UpdateAllPersonsView(PersonsView control)
        {
            AllPersons = DataWorker.GetAllPersons();
            control.PersonsListView.ItemsSource = null;
            control.PersonsListView.Items.Clear();
            control.PersonsListView.ItemsSource = AllPersons;
            control.PersonsListView.Items.Refresh();
        }


        #endregion

        #region Deletes

        private void SetNullValuesToProperties()
        {
            //для пользователя
            PersonName = null;
            PersonInfo = null;
            PersonPhone = null;
            PersonType = null;
        }

        private void SetNullPersonTextBoxes(UserControl control)
        {
            SetContentToNull(control, "PersonInfoTextBox");
            SetContentToNull(control, "PersonNameTextBox");
            SetContentToNull(control, "PersonPhoneTextBox");
            //SetContentToNull(control, "PersonTypeComboBox");
        }


        #endregion
        #endregion



        public event PropertyChangedEventHandler PropertyChanged;

        private void SetRedBlockControl(UserControl control, string blockName)
        {
            var block = control.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Crimson;
        }

        private void SetContentToNull(UserControl control, string blockName)
        {
            var block = control.FindName(blockName) as TextBox;
            block.Text = null;
        }
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
