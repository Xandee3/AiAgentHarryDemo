using System.Windows;

namespace AIAgentHarryDemo
{
    public partial class ContactDialog : Window
    {
        internal string Salutation => SalutationTextBox.Text.Trim();

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
                FormatLine("Anrede", Salutation),
                FormatLine("Name", $"{FirstName} {LastName}".Trim()),
                FormatLine("Position", Position),
                FormatLine("Telefonnummer", Phone),
                FormatLine("E-Mail-Adresse", Email),
                FormatLine("Bemerkungen", Remarks)
            });

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
