﻿using System.Windows;
using System.Windows.Input;

namespace _fileOrganizer
{
    /// <summary>
    /// Interaction logic for CreateWindow.xaml
    /// </summary>
    public partial class CreateWindow: Window
    {
        public CreateWindow ()
        {
            try
            {
                InitializeComponent ();
            }

            catch (Exception xException)
            {
                Utility.TryHandleException (this, xException);
            }
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

        private void NameTextBoxKeyDown (object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    var xViewModel = (CreateWindowViewModel) DataContext;

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

        private void NameTextBoxTextChanged (object sender, RoutedEventArgs e)
        {
            try
            {
                var xViewModel = (CreateWindowViewModel) DataContext;
                string? xTrimmedName = xViewModel.Name?.Trim ();

                if (string.IsNullOrWhiteSpace (xTrimmedName) == false)
                {
                    if (xViewModel.ExistingNames == null || xViewModel.ExistingNames.Contains (xTrimmedName, StringComparer.OrdinalIgnoreCase) == false)
                    {
                        xViewModel.ErrorMessage = null;
                        xViewModel.CanCreate = true;
                        return;
                    }

                    else
                    {
                        xViewModel.ErrorMessage = "Name already exists.";
                        xViewModel.CanCreate = false;
                    }
                }

                else
                {
                    xViewModel.ErrorMessage = null;
                    xViewModel.CanCreate = false;
                }
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
                var xViewModel = (CreateWindowViewModel) DataContext;
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
