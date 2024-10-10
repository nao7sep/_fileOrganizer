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
                    var xGroup = new Group { Name = xCreateWindowViewModel.Name };

                    xViewModel.Groups ??= [];
                    Utility.InsertItemInOrder (xViewModel.Groups, xGroup, x => x.Name);

                    GroupsListBox.SelectedItem = xGroup;
                    GroupsListBox.ScrollIntoView (xGroup);
                }
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }
    }
}
