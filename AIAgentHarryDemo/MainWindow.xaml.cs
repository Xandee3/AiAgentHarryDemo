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
            ContactsDataGrid.SelectedItems.Clear();

            var searchTerm = SearchTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return;
            }

            Contact? firstMatch = null;

            foreach (var item in ContactsDataGrid.Items)
            {
                if (item is not Contact contact)
                {
                    continue;
                }

                if (!MatchesSearchTerm(contact, searchTerm))
                {
                    continue;
                }

                ContactsDataGrid.SelectedItems.Add(contact);
                firstMatch ??= contact;
            }

            if (firstMatch is not null)
            {
                ContactsDataGrid.ScrollIntoView(firstMatch);
            }
        }

        private static bool MatchesSearchTerm(Contact contact, string searchTerm)
        {
            return ContainsSearchTerm(contact.Salutation, searchTerm)
                || ContainsSearchTerm(contact.FirstName, searchTerm)
                || ContainsSearchTerm(contact.LastName, searchTerm)
                || ContainsSearchTerm(contact.Position, searchTerm)
                || ContainsSearchTerm(contact.PhoneNumber, searchTerm)
                || ContainsSearchTerm(contact.EmailAddress, searchTerm)
                || ContainsSearchTerm(contact.Remarks, searchTerm);
        }

        private static bool ContainsSearchTerm(string value, string searchTerm)
        {
            return value.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase);
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
