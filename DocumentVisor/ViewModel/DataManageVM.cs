using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DocumentVisor.Annotations;
using DocumentVisor.Model;

namespace DocumentVisor.ViewModel
{
    public class DataManageVm :INotifyPropertyChanged
    {
        private List<Person> _allPersons = DataWorker.GetAllPersons();

        public List<Person> AllPersons
        {
            get => _allPersons;
            set
            {
                _allPersons = value;
                OnPropertyChanged(nameof(AllPersons));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
