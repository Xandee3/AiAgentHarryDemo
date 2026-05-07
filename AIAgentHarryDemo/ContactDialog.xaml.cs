using System.Windows;

namespace AIAgentHarryDemo
{
    public partial class ContactDialog : Window
    {
        internal string FirstName => FirstNameTextBox.Text;

        internal string LastName => LastNameTextBox.Text;

        public ContactDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
