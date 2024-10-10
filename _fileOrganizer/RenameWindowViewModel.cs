using System.ComponentModel;

namespace _fileOrganizer
{
    public class RenameWindowViewModel: INotifyPropertyChanged
    {
        private string? _title;

        public string? Title
        {
            get => _title;

            set
            {
                if (string.Equals (_title, value, StringComparison.Ordinal) == false)
                {
                    _title = value;
                    OnPropertyChanged (nameof (Title));
                }
            }
        }

        private string? _message;

        public string? Message
        {
            get => _message;

            set
            {
                if (string.Equals (_message, value, StringComparison.Ordinal) == false)
                {
                    _message = value;
                    OnPropertyChanged (nameof (Message));
                }
            }
        }

        public IEnumerable <string?>? ExistingNames { get; set; }

        private string? _currentName;

        public string? CurrentName
        {
            get => _currentName;

            set
            {
                if (string.Equals (_currentName, value, StringComparison.Ordinal) == false)
                {
                    _currentName = value;
                    OnPropertyChanged (nameof (CurrentName));
                }
            }
        }

        private string? _newName;

        public string? NewName
        {
            get => _newName;

            set
            {
                if (string.Equals (_newName, value, StringComparison.Ordinal) == false)
                {
                    _newName = value;
                    OnPropertyChanged (nameof (NewName));
                }
            }
        }

        private string? _errorMessage;

        public string? ErrorMessage
        {
            get => _errorMessage;

            set
            {
                if (string.Equals (_errorMessage, value, StringComparison.Ordinal) == false)
                {
                    _errorMessage = value;
                    OnPropertyChanged (nameof (ErrorMessage));
                }
            }
        }

        private bool _canRename;

        public bool CanRename
        {
            get => _canRename;

            set
            {
                if (_canRename != value)
                {
                    _canRename = value;
                    OnPropertyChanged (nameof (CanRename));
                }
            }
        }

        public bool? IsRenamed { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged (string propertyName) => PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
    }
}
