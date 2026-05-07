using System.Windows;
using System.Windows.Controls;

namespace AIAgentHarryDemo
{
    public partial class ContactDialog : Window
    {
        public Contact? Contact { get; private set; }

        public ContactDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Contact = new Contact
            {
                Salutation = GetSelectedSalutation(),
                FirstName = FirstNameTextBox.Text.Trim(),
                LastName = LastNameTextBox.Text.Trim(),
                PhoneNumber = PhoneNumberTextBox.Text.Trim(),
                EmailAddress = EmailAddressTextBox.Text.Trim(),
                Position = PositionTextBox.Text.Trim(),
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
    }
}
