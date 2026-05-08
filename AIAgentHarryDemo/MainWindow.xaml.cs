using System.Collections.ObjectModel;
using System.Windows;

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
    }
}
