using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DocumentVisor.Model
{
    public class PersonType : IDataField, INotifyPropertyChanged, IEqualityComparer<PersonType>
    {
        private int _id;
        private string _name;
        private string _info;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Info
        {
            get => _info;
            set
            {
                _info = value;
                OnPropertyChanged(nameof(Info));
            }
        }

        public override string ToString()
        {
            return $"{Name}\n({Info})";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [Annotations.NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Equals(PersonType x, PersonType y)
        {
            return y != null && x != null && x.Name.Equals(y.Name) && x.Info.Equals(y.Info);
        }

        public int GetHashCode(PersonType obj)
        {
            return obj.Name.GetHashCode() + obj.Info.GetHashCode() + obj.Id;
        }
    }
}