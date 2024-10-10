using System.Collections.ObjectModel;
using System.ComponentModel;

namespace _fileOrganizer
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        private ObservableCollection <Group>? _groups;

        public ObservableCollection <Group>? Groups
        {
            get => _groups;

            set
            {
                if (_groups != value)
                {
                    _groups = value;
                    OnPropertyChanged (nameof (Groups));
                }
            }
        }

        private Group? _selectedGroup;

        public Group? SelectedGroup
        {
            get => _selectedGroup;

            set
            {
                if (_selectedGroup != value)
                {
                    _selectedGroup = value;
                    OnPropertyChanged (nameof (SelectedGroup));
                    OnPropertyChanged (nameof (SelectedGroupDestinations));
                }
            }
        }

        public ObservableCollection <Destination>? SelectedGroupDestinations => SelectedGroup?.Destinations;

        private Destination? _selectedDestination;

        public Destination? SelectedDestination
        {
            get => _selectedDestination;

            set
            {
                if (_selectedDestination != value)
                {
                    _selectedDestination = value;
                    OnPropertyChanged (nameof (SelectedDestination));
                    OnPropertyChanged (nameof (SelectedDestinationSubdirectories));
                }
            }
        }

        public ObservableCollection <Subdirectory>? SelectedDestinationSubdirectories => SelectedDestination?.Subdirectories;

        private Subdirectory? _selectedSubdirectory;

        public Subdirectory? SelectedSubdirectory
        {
            get => _selectedSubdirectory;

            set
            {
                if (_selectedSubdirectory != value)
                {
                    _selectedSubdirectory = value;
                    OnPropertyChanged (nameof (SelectedSubdirectory));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged (string propertyName) => PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
    }
}
