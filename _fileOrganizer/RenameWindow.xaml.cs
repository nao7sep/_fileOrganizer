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

        private void NewNameTextBoxKeyDown (object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    var xViewModel = (RenameWindowViewModel) DataContext;

                    if (xViewModel.CanRename)
                        RenameButtonClick (sender, e);
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
                    if (xViewModel.ExistingNames == null || xViewModel.ExistingNames.Contains (xTrimmedName, StringComparer.OrdinalIgnoreCase) == false ||
                        string.Equals (xViewModel.CurrentName, xTrimmedName, StringComparison.OrdinalIgnoreCase))
                    {
                        xViewModel.ErrorMessage = null;
                        xViewModel.CanRename = true;
                        return;
                    }

                    else
                    {
                        xViewModel.ErrorMessage = "Name already exists.";
                        xViewModel.CanRename = false;
                    }
                }

                else
                {
                    xViewModel.ErrorMessage = null;
                    xViewModel.CanRename = false;
                }
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }

        private void RenameButtonClick (object sender, RoutedEventArgs e)
        {
            try
            {
                var xViewModel = (RenameWindowViewModel) DataContext;
                xViewModel.IsRenamed = true;
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
                xViewModel.IsRenamed = false;
                Close ();
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }
    }
}
