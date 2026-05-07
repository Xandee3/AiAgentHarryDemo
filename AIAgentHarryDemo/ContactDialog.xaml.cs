using System.Windows;
using System.Windows.Controls;

namespace AIAgentHarryDemo
{
    public partial class ContactDialog : Window
    {
        internal string Salutation =>
            (SalutationComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString()?.Trim() ?? string.Empty;

        internal string FirstName => FirstNameTextBox.Text.Trim();

        internal string LastName => LastNameTextBox.Text.Trim();

        internal string Position => PositionTextBox.Text.Trim();

        internal string Phone => PhoneTextBox.Text.Trim();

        internal string Email => EmailTextBox.Text.Trim();

        internal string Remarks => RemarksTextBox.Text.Trim();

        internal string ContactText => string.Join(
            "\n",
            new[]
            {
                ("Anrede", Salutation),
                ("Vorname", FirstName),
                ("Name", LastName),
                ("Position", Position),
                ("Telefonnummer", Phone),
                ("E-Mail-Adresse", Email),
                ("Bemerkungen", Remarks)
            }
            .Where(line => !string.IsNullOrWhiteSpace(line.Item2))
            .Select(line => FormatLine(line.Item1, line.Item2)));

        public ContactDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private static string FormatLine(string label, string value)
        {
            return $"{label}: {value}";
        }
    }
}
