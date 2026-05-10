using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace AIAgentHarryDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Contact> contacts = new();
        private readonly ICollectionView contactsView;

        public MainWindow()
        {
            InitializeComponent();
            contactsView = CollectionViewSource.GetDefaultView(contacts);
            ContactsDataGrid.ItemsSource = contactsView;
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
            ApplyContactFilter();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                return;
            }

            ApplyContactFilter();
            e.Handled = true;
        }

        private void ApplyContactFilter()
        {
            ContactsDataGrid.SelectedItem = null;

            var searchTerm = SearchTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                contactsView.Filter = null;
                contactsView.Refresh();
                return;
            }

            contactsView.Filter = item => item is Contact contact && MatchesSearchTerm(contact, searchTerm);
            contactsView.Refresh();

            if (contactsView.Cast<Contact>().FirstOrDefault() is Contact firstMatch)
            {
                ContactsDataGrid.SelectedItem = firstMatch;
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
