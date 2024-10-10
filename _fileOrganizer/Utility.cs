using System.Collections.ObjectModel;
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

        public static ViewModelType InitializeWindow <ViewModelType> (dynamic window, Window owner, string title, string message)
        {
            window.Owner = owner;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            dynamic xViewModel = (ViewModelType) window.DataContext;
            xViewModel.Title = title;
            xViewModel.Message = message;

            return xViewModel;
        }

        // There seems to be no XAML-only way to sort ListBox items case-insensitively.
        // Also considering serializing the entire view model to JSON,
        // I've chosen to keep the ObservableCollection sorted.

        public static void InsertItemInOrder <ItemType> (ObservableCollection <ItemType> collection, ItemType item, Func <ItemType, string?> selector)
        {
            int xIndex = -1;

            for (int temp = 0; temp < collection.Count; temp ++)
            {
                if (string.Compare (selector (item), selector (collection [temp]), StringComparison.OrdinalIgnoreCase) < 0)
                {
                    xIndex = temp;
                    break;
                }
            }

            if (xIndex >= 0)
                collection.Insert (xIndex, item);

            else collection.Add (item);
        }
    }
}
