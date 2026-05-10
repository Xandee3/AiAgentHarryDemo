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

            foreach (var contact in ContactStore.Load())
            {
                contacts.Add(contact);
            }
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
                ContactStore.Save(contacts);
            }
        }

        private void ContactsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSelectedContactDisplay(ContactsDataGrid.SelectedItem as Contact);
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

        private void UpdateSelectedContactDisplay(Contact? contact)
        {
            if (contact is null)
            {
                SelectedContactBorder.Visibility = Visibility.Collapsed;
                return;
            }

            SelectedSalutationTextBlock.Text = contact.Salutation;
            SelectedFirstNameTextBlock.Text = contact.FirstName;
            SelectedLastNameTextBlock.Text = contact.LastName;
            SelectedPositionTextBlock.Text = contact.Position;
            SelectedPhoneNumberTextBlock.Text = contact.PhoneNumber;
            SelectedEmailAddressTextBlock.Text = contact.EmailAddress;
            SelectedRemarksTextBlock.Text = contact.Remarks;
            SelectedContactBorder.Visibility = Visibility.Visible;
        }
    }
}
