using System.Windows;
using System.Windows.Controls;

namespace AIAgentHarryDemo
{
    public partial class ContactDialog : Window
    {
        public Contact? CreatedContact { get; private set; }

        public ContactDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            CreatedContact = new Contact
            {
                Salutation = GetSelectedComboBoxText(SalutationComboBox),
                FirstName = FirstNameTextBox.Text.Trim(),
                LastName = LastNameTextBox.Text.Trim(),
                Position = PositionTextBox.Text.Trim(),
                PhoneNumber = PhoneNumberTextBox.Text.Trim(),
                EmailAddress = EmailAddressTextBox.Text.Trim(),
                Notes = NotesTextBox.Text.Trim()
            };

            DialogResult = true;
        }

        private static string GetSelectedComboBoxText(ComboBox comboBox)
        {
            return comboBox.SelectedItem is ComboBoxItem item
                ? item.Content?.ToString() ?? string.Empty
                : comboBox.Text;
        }
    }
}
