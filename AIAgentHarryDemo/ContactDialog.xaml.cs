using System.Windows;
using System.Windows.Controls;

namespace AIAgentHarryDemo
{
    public partial class ContactDialog : Window
    {
        public ContactDialog()
        {
            InitializeComponent();
        }

        internal ContactDialog(Contact contact)
            : this()
        {
            ApplyContact(contact);
        }

        internal Contact? Contact { get; private set; }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Contact = new Contact
            {
                Salutation = GetSelectedSalutation(),
                FirstName = FirstNameTextBox.Text.Trim(),
                LastName = LastNameTextBox.Text.Trim(),
                Position = PositionTextBox.Text.Trim(),
                PhoneNumber = PhoneNumberTextBox.Text.Trim(),
                EmailAddress = EmailAddressTextBox.Text.Trim(),
                Remarks = RemarksTextBox.Text.Trim()
            };

            DialogResult = true;
        }

        private string GetSelectedSalutation()
        {
            return SalutationComboBox.SelectedItem is ComboBoxItem item
                ? item.Content?.ToString() ?? string.Empty
                : string.Empty;
        }

        private void ApplyContact(Contact contact)
        {
            SetSelectedSalutation(contact.Salutation);
            FirstNameTextBox.Text = contact.FirstName;
            LastNameTextBox.Text = contact.LastName;
            PositionTextBox.Text = contact.Position;
            PhoneNumberTextBox.Text = contact.PhoneNumber;
            EmailAddressTextBox.Text = contact.EmailAddress;
            RemarksTextBox.Text = contact.Remarks;
        }

        private void SetSelectedSalutation(string salutation)
        {
            foreach (var item in SalutationComboBox.Items)
            {
                if (item is ComboBoxItem comboBoxItem
                    && string.Equals(comboBoxItem.Content?.ToString(), salutation, System.StringComparison.CurrentCulture))
                {
                    SalutationComboBox.SelectedItem = comboBoxItem;
                    return;
                }
            }

            SalutationComboBox.SelectedIndex = 0;
        }
    }
}
