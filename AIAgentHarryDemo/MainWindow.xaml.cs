using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace AIAgentHarryDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Contact> contacts = new();

        public MainWindow()
        {
            InitializeComponent();
            ContactsDataGrid.ItemsSource = contacts;
        }

        private void ContactMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContactDialog
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true && dialog.Contact is not null)
            {
                contacts.Add(dialog.Contact);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SelectSearchResults();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                return;
            }

            SelectSearchResults();
            e.Handled = true;
        }

        private void SelectSearchResults()
        {
            ResultsListBox.SelectedItems.Clear();

            var searchTerm = SearchTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return;
            }

            object? firstMatch = null;

            foreach (var item in ResultsListBox.Items)
            {
                var itemText = item is ListBoxItem listBoxItem
                    ? listBoxItem.Content?.ToString()
                    : item.ToString();

                if (itemText?.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) != true)
                {
                    continue;
                }

                ResultsListBox.SelectedItems.Add(item);
                firstMatch ??= item;
            }

            if (firstMatch is not null)
            {
                ResultsListBox.ScrollIntoView(firstMatch);
            }
        }
    }
}
