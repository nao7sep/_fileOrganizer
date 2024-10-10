namespace _fileOrganizer
{
    public class File
    {
        public string? Path { get; set; }

        public override string? ToString () => Path;
    }
}
