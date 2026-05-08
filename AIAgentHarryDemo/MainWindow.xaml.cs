using System.Collections.ObjectModel;
using System.Windows;

namespace AIAgentHarryDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Contact> Contacts { get; } = [];

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ContactMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContactDialog
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true && dialog.Contact is not null)
            {
                Contacts.Add(dialog.Contact);
            }
        }
    }
}
