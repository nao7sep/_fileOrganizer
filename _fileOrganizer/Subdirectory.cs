namespace _fileOrganizer
{
    public class Subdirectory
    {
        public string? Path { get; set; }

        public override string? ToString () => System.IO.Path.GetFileName (Path);
    }
}
