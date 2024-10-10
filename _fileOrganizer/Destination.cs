using System.Collections.ObjectModel;

namespace _fileOrganizer
{
    public class Destination
    {
        public string? Name { get; set; }

        public ObservableCollection <Subdirectory>? Subdirectories { get; set; }
    }
}
