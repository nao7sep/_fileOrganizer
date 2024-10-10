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

        private void CreateGroupButtonClicked (object sender, RoutedEventArgs e)
        {
            try
            {
                var xViewModel = (MainWindowViewModel) DataContext;

                CreateWindow xCreateWindow = new ();
                xCreateWindow.Owner = this;
                xCreateWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                var xCreateWindowViewModel = (CreateWindowViewModel) xCreateWindow.DataContext;
                xCreateWindowViewModel.Title = "Create Group";
                xCreateWindowViewModel.Message = "Enter the name of the group.";
                xCreateWindow.ShowDialog ();

                if (xCreateWindowViewModel.IsCreated == true)
                {
                    xViewModel.Groups ??= [];
                    xViewModel.Groups.Add (new Group { Name = xCreateWindowViewModel.Name });
                }
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }
    }
}
