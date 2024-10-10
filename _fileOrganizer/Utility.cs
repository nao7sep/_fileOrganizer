using System.Windows;
using yyLib;

namespace _fileOrganizer
{
    public static class Utility
    {
        public static bool TryHandleException (Window? owner, Exception exception)
        {
            try
            {
                yyLogger.Default.WriteException (exception);

                if (owner != null)
                    MessageBox.Show (owner, exception.ToString (), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                else MessageBox.Show (exception.ToString (), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return true;
            }

            catch
            {
                return false;
            }
        }
    }
}
