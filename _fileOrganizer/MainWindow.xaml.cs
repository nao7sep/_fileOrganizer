using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using yyLib;

namespace _fileOrganizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    {
        public MainWindow ()
        {
            try
            {
                InitializeComponent ();

                if (System.IO.File.Exists (Utility.DestinationsFilePath))
                {
                    string xJsonString = System.IO.File.ReadAllText (Utility.DestinationsFilePath, Encoding.UTF8);
                    DataContext = JsonSerializer.Deserialize <MainWindowViewModel> (xJsonString, yyJson.DefaultDeserializationOptions);
                }
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }

        private void _Save ()
        {
            var xViewModel = (MainWindowViewModel) DataContext;
            string xJsonString = JsonSerializer.Serialize (xViewModel, yyJson.DefaultSerializationOptions);
            System.IO.File.WriteAllText (Utility.DestinationsFilePath, xJsonString, Encoding.UTF8);

            Directory.CreateDirectory (Utility.BackupsDirectoryPath);
            string xBackupFilePath = Path.Join (Utility.BackupsDirectoryPath, $"Destinations-{DateTime.UtcNow:yyyyMMdd'T'HHmmss'Z'}.json");
            System.IO.File.WriteAllText (xBackupFilePath, xJsonString, Encoding.UTF8);
        }

        private void GroupsListBoxKeyDown (object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (e.Key == System.Windows.Input.Key.Delete)
                {
                    var xViewModel = (MainWindowViewModel) DataContext;

                    if (xViewModel.SelectedGroup != null)
                        DeleteGroupButtonClick (sender, e);
                }
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
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
                    _Save ();

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
                    _Save ();

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
                    _Save ();

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

        private void DestinationsListBoxKeyDown (object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (e.Key == System.Windows.Input.Key.Delete)
                {
                    var xViewModel = (MainWindowViewModel) DataContext;

                    if (xViewModel.SelectedDestination != null)
                        RemoveDestinationButtonClick (sender, e);
                }
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }

        private void DestinationsListBoxSelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var xViewModel = (MainWindowViewModel) DataContext;

                if (xViewModel.SelectedDestination != null)
                {
                    if (Directory.Exists (xViewModel.SelectedDestination.Path) == false)
                        System.Windows.MessageBox.Show (this, "Destination does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    if (xViewModel.SelectedGroupDestinations != null &&
                        xViewModel.SelectedGroupDestinations.Any (x => string.Equals (x.Path, xDialog.SelectedPath, StringComparison.OrdinalIgnoreCase)))
                    {
                        System.Windows.MessageBox.Show (this, "Destination already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var xDestination = new Destination { Path = xDialog.SelectedPath };

                    xViewModel.SelectedGroupDestinations ??= [];
                    Utility.InsertItemInOrder (xViewModel.SelectedGroupDestinations, xDestination, x => x.Path);
                    _Save ();

                    DestinationsListBox.SelectedItem = xDestination;
                    DestinationsListBox.ScrollIntoView (xDestination);
                }
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }

        private void RemoveDestinationButtonClick (object sender, RoutedEventArgs e)
        {
            try
            {
                var xViewModel = (MainWindowViewModel) DataContext;
                string xMessage = $"Are you sure you want to delete the destination '{xViewModel.SelectedDestination!.Path}'?";

                if (System.Windows.MessageBox.Show (this, xMessage, "Delete Destination", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    var xDestination = xViewModel.SelectedDestination;
                    var xAdjacentDestination = Utility.GetAdjacentItem (xViewModel.SelectedGroupDestinations!, xDestination);
                    xViewModel.SelectedGroupDestinations!.Remove (xDestination);
                    _Save ();

                    if (xAdjacentDestination != null)
                    {
                        DestinationsListBox.SelectedItem = xAdjacentDestination;
                        DestinationsListBox.ScrollIntoView (xAdjacentDestination);
                    }
                }
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }
    }
}
