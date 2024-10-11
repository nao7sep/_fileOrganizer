using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace _fileOrganizer
{
    public class Destination
    {
        public string? Path { get; set; }

        [JsonIgnore]
        public ObservableCollection <Subdirectory>? Subdirectories { get; set; }

        public override string? ToString () => Path;
    }
}
