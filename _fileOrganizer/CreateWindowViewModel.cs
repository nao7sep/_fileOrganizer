using System.ComponentModel;

namespace _fileOrganizer
{
    public class CreateWindowViewModel: INotifyPropertyChanged
    {
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
