using System.IO;
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
                xCreateWindowViewModel.ExistingNames = xViewModel.Groups?.Select (x => x.Name);
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

        private void RenameGroupButtonClick (object sender, RoutedEventArgs e)
        {
            try
            {
                var xViewModel = (MainWindowViewModel) DataContext;

                RenameWindow xRenameWindow = new ();
                var xRenameWindowViewModel = Utility.InitializeWindow <RenameWindowViewModel> (xRenameWindow, this, "Rename Group", "Enter the new name of the group.");
                xRenameWindowViewModel.ExistingNames = xViewModel.Groups?.Select (x => x.Name);
                xRenameWindowViewModel.CurrentName = xViewModel.SelectedGroup?.Name;
                xRenameWindow.ShowDialog ();

                if (xRenameWindowViewModel.IsRenamed == true)
                {
                    var xGroup = xViewModel.SelectedGroup;
                    xGroup!.Name = xRenameWindowViewModel.NewName;
                    xViewModel.Groups!.Remove (xGroup);
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

        private void DeleteGroupButtonClick (object sender, RoutedEventArgs e)
        {
            try
            {
                var xViewModel = (MainWindowViewModel) DataContext;
                string xMessage = $"Are you sure you want to delete the group '{xViewModel.SelectedGroup!.Name}'?";

                if (System.Windows.MessageBox.Show (this, xMessage, "Delete Group", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    var xGroup = xViewModel.SelectedGroup;
                    var xAdjacentGroup = Utility.GetAdjacentItem (xViewModel.Groups!, xGroup);
                    xViewModel.Groups!.Remove (xGroup);

                    if (xAdjacentGroup != null)
                    {
                        GroupsListBox.SelectedItem = xAdjacentGroup;
                        GroupsListBox.ScrollIntoView (xAdjacentGroup);
                    }
                }
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }

        private void AddDestinationButtonClick (object sender, RoutedEventArgs e)
        {
            try
            {
                var xViewModel = (MainWindowViewModel) DataContext;

                using FolderBrowserDialog xDialog = new ();
                // xDialog.Description = "Select the destination folder."; // Doesnt look good.
                xDialog.ShowNewFolderButton = true;
                var xResult = xDialog.ShowDialog ();

                if (xResult == System.Windows.Forms.DialogResult.OK && Directory.Exists (xDialog.SelectedPath))
                {
                    var xDestination = new Destination { Path = xDialog.SelectedPath };

                    xViewModel.SelectedGroupDestinations ??= [];
                    Utility.InsertItemInOrder (xViewModel.SelectedGroupDestinations, xDestination, x => x.Path);

                    DestinationsListBox.SelectedItem = xDestination;
                    DestinationsListBox.ScrollIntoView (xDestination);
                }
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }
    }
}
