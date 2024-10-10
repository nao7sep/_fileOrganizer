using System.Windows;

namespace _fileOrganizer
{
    /// <summary>
    /// Interaction logic for CreateWindow.xaml
    /// </summary>
    public partial class CreateWindow: Window
    {
        public CreateWindow ()
        {
            InitializeComponent ();
        }

        private void WindowLoaded (object sender, RoutedEventArgs e)
        {
            try
            {
                NameTextBox.Focus ();
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }

        private void NameTextBoxTextChanged (object sender, RoutedEventArgs e)
        {
            try
            {
                var xViewModel = (CreateWindowViewModel) DataContext;
                string? xTrimmedName = xViewModel.Name?.Trim ();

                if (string.IsNullOrWhiteSpace (xTrimmedName) == false)
                {
                    var xExistingNames = ((MainWindowViewModel) Owner.DataContext).Groups?.Select (x => x.Name);

                    if (xExistingNames == null || xExistingNames.Contains (xTrimmedName, StringComparer.OrdinalIgnoreCase) == false)
                    {
                        xViewModel.ErrorMessage = null;
                        xViewModel.CanCreate = true;
                        return;
                    }

                    else xViewModel.ErrorMessage = "Name already exists.";
                }

                xViewModel.CanCreate = false;
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }

        private void CreateButtonClicked (object sender, RoutedEventArgs e)
        {
            try
            {
                var xViewModel = (CreateWindowViewModel) DataContext;
                xViewModel.IsCreated = true;
                Close ();
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }

        private void CancelButtonClicked (object sender, RoutedEventArgs e)
        {
            try
            {
                var xViewModel = (CreateWindowViewModel) DataContext;
                xViewModel.IsCreated = false;
                Close ();
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }
    }
}
