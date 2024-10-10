using System.ComponentModel;

namespace _fileOrganizer
{
    public class CreateWindowViewModel: INotifyPropertyChanged
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

        public IEnumerable <string>? ExistingNames { get; set; }

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

        private string? _name;

        public string? Name
        {
            get => _name;

            set
            {
                if (string.Equals (_name, value, StringComparison.Ordinal) == false)
                {
                    _name = value;
                    OnPropertyChanged (nameof (Name));
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged (string propertyName) => PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
    }
}
