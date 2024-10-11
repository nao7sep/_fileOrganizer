using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Serialization;
using yyLib;

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
                    OnPropertyChanged (nameof (SelectedGroup));
                    OnPropertyChanged (nameof (SelectedGroupDestinations));
                    OnPropertyChanged (nameof (SelectedDestination));
                    OnPropertyChanged (nameof (SelectedDestinationSubdirectories));
                    OnPropertyChanged (nameof (SelectedSubdirectory));
                }
            }
        }

        private Group? _selectedGroup;

        [JsonIgnore]
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
                    OnPropertyChanged (nameof (SelectedDestination));
                    OnPropertyChanged (nameof (SelectedDestinationSubdirectories));
                    OnPropertyChanged (nameof (SelectedSubdirectory));
                }
            }
        }

        [JsonIgnore]
        public ObservableCollection <Destination>? SelectedGroupDestinations
        {
            get => SelectedGroup?.Destinations;

            set
            {
                if (SelectedGroup == null)
                    throw new yyInvalidOperationException ("SelectedGroup is null.");

                if (SelectedGroup.Destinations != value)
                {
                    SelectedGroup.Destinations = value;
                    OnPropertyChanged (nameof (SelectedGroupDestinations));
                    OnPropertyChanged (nameof (SelectedDestination));
                    OnPropertyChanged (nameof (SelectedDestinationSubdirectories));
                    OnPropertyChanged (nameof (SelectedSubdirectory));
                }
            }
        }

        private Destination? _selectedDestination;

        [JsonIgnore]
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
                    OnPropertyChanged (nameof (SelectedSubdirectory));
                }
            }
        }

        [JsonIgnore]
        public ObservableCollection <Subdirectory>? SelectedDestinationSubdirectories
        {
            get => SelectedDestination?.Subdirectories;

            set
            {
                if (SelectedDestination == null)
                    throw new yyInvalidOperationException ("SelectedDestination is null.");

                if (SelectedDestination.Subdirectories != value)
                {
                    SelectedDestination.Subdirectories = value;
                    OnPropertyChanged (nameof (SelectedDestinationSubdirectories));
                    OnPropertyChanged (nameof (SelectedSubdirectory));
                }
            }
        }

        private Subdirectory? _selectedSubdirectory;

        [JsonIgnore]
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

        private ObservableCollection <File>? _files;

        [JsonIgnore]
        public ObservableCollection <File>? Files
        {
            get => _files;

            set
            {
                if (_files != value)
                {
                    _files = value;
                    OnPropertyChanged (nameof (Files));
                    OnPropertyChanged (nameof (SelectedFile));
                }
            }
        }

        private File? _selectedFile;

        [JsonIgnore]
        public File? SelectedFile
        {
            get => _selectedFile;

            set
            {
                if (_selectedFile != value)
                {
                    _selectedFile = value;
                    OnPropertyChanged (nameof (SelectedFile));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged (string propertyName) => PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
    }
}
