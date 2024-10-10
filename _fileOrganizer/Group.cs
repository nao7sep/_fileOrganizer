using System.Collections.ObjectModel;

namespace _fileOrganizer
{
    public class Group
    {
        public string? Name { get; set; }

        public ObservableCollection <Destination>? Destinations { get; set; }
    }
}
