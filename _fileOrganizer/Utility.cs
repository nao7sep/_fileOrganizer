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
                    System.Windows.MessageBox.Show (owner, exception.ToString (), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                else System.Windows.MessageBox.Show (exception.ToString (), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

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

        public static ItemType? GetAdjacentItem <ItemType> (ObservableCollection <ItemType> collection, ItemType item) where ItemType: class
        {
            int xIndex = collection.IndexOf (item);

            if (xIndex < 0)
                throw new yyArgumentException ("The current item is not in the collection."); // Should never happen.

            // At least one item is after the current one.
            if (xIndex + 1 < collection.Count)
                return collection [xIndex + 1];

            // The current item is the last one and there are at least two items.
            if (xIndex == collection.Count - 1 && collection.Count >= 2)
                return collection [xIndex - 1];

            return null;
        }
    }
}
