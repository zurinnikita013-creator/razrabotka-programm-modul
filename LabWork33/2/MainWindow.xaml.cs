using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserDataGridApp;

namespace _2
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var users = new List<User>
            {
                new User { Address = "user1@example.com", Login = "user1", Password = "pass1", Category = "email", IsArchived = false },
                new User { Address = "user2@example.com", Login = "user2", Password = "pass2", Category = "БД", IsArchived = true },
                new User { Address = "user3@example.com", Login = "user3", Password = "pass3", Category = "сайт", IsArchived = false },
                new User { Address = "user4@example.com", Login = "user4", Password = "pass4", Category = "email", IsArchived = false },
                new User { Address = "user5@example.com", Login = "user5", Password = "pass5", Category = "БД", IsArchived = true }
            };
            UsersDataGrid.ItemsSource = users;
        }

        private void CopyPassword_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button.DataContext as User;
            Clipboard.SetText(user.Password);
        }
    }
}