using System.Collections.ObjectModel;
using System.Windows;

namespace AIAgentHarryDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Contact> _contacts = [];

        public MainWindow()
        {
            InitializeComponent();
            ContactsDataGrid.ItemsSource = _contacts;
        }

        private void ContactMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContactDialog
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true && dialog.CreatedContact is not null)
            {
                _contacts.Add(dialog.CreatedContact);
            }
        }
    }
}
