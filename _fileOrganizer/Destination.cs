using System.Collections.ObjectModel;

namespace _fileOrganizer
{
    public class Destination
    {
        public string? Path { get; set; }

        public ObservableCollection <Subdirectory>? Subdirectories { get; set; }
    }
}
