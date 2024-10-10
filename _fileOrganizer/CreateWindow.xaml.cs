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

        private void NameTextBoxTextChanged (object sender, RoutedEventArgs e)
        {
            try
            {
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
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
        }
    }
}
