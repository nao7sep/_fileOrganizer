using System.Windows;
using System.Windows.Input;

namespace _fileOrganizer
{
    /// <summary>
    /// Interaction logic for RenameWindow.xaml
    /// </summary>
    public partial class RenameWindow: Window
    {
        public RenameWindow ()
        {
            InitializeComponent ();
        }

        private void WindowLoaded (object sender, RoutedEventArgs e)
        {
            try
            {
                NewNameTextBox.Focus ();
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }

        private void NewNameTextBoxKeyDown (object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    var xViewModel = (RenameWindowViewModel) DataContext;

                    if (xViewModel.CanCreate)
                        CreateButtonClick (sender, e);
                }

                else if (e.Key == Key.Escape)
                    CancelButtonClick (sender, e);
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }

        private void NewNameTextBoxTextChanged (object sender, RoutedEventArgs e)
        {
            try
            {
                var xViewModel = (RenameWindowViewModel) DataContext;
                string? xTrimmedName = xViewModel.NewName?.Trim ();

                if (string.IsNullOrWhiteSpace (xTrimmedName) == false)
                {
                    if (xViewModel.ExistingNames == null || xViewModel.ExistingNames.Contains (xTrimmedName, StringComparer.OrdinalIgnoreCase) == false)
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

        private void CreateButtonClick (object sender, RoutedEventArgs e)
        {
            try
            {
                var xViewModel = (RenameWindowViewModel) DataContext;
                xViewModel.IsCreated = true;
                Close ();
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }

        private void CancelButtonClick (object sender, RoutedEventArgs e)
        {
            try
            {
                var xViewModel = (RenameWindowViewModel) DataContext;
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
