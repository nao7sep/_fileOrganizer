using System.Windows;

namespace _fileOrganizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    {
        public MainWindow ()
        {
            InitializeComponent ();
        }

        private void CreateGroupButtonClick (object sender, RoutedEventArgs e)
        {
            try
            {
                var xViewModel = (MainWindowViewModel) DataContext;

                CreateWindow xCreateWindow = new ();
                var xCreateWindowViewModel = Utility.InitializeWindow <CreateWindowViewModel> (xCreateWindow, this, "Create Group", "Enter the name of the group.");
                xCreateWindow.ShowDialog ();

                if (xCreateWindowViewModel.IsCreated == true)
                {
                    xViewModel.Groups ??= [];

                    // There seems to be no XAML-only way to sort ListBox items case-insensitively.
                    // Also considering serializing the entire view model to JSON,
                    // I've chosen to keep the ObservableCollection sorted.

                    int xCount = xViewModel.Groups.Count,
                        xIndex = -1;

                    for (int temp = 0; temp < xCount; temp ++)
                    {
                        if (xCreateWindowViewModel.Name!.CompareTo (xViewModel.Groups [temp].Name) < 0)
                        {
                            xIndex = temp;
                            break;
                        }
                    }

                    var xGroup = new Group { Name = xCreateWindowViewModel.Name };

                    if (xIndex >= 0)
                        xViewModel.Groups.Insert (xIndex, xGroup);

                    else xViewModel.Groups.Add (xGroup);
                }
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }
    }
}
