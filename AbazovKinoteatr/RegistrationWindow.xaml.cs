using System.Windows;
using System.Linq;

namespace AbazovKinoteatr.Windows
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            if (Register(LoginBox.Text, PassBox.Password, NameBox.Text))
            {
                MessageBox.Show("Регистрация успешна");
                new LoginWindow().Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка регистрации (возможно, логин занят)");
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }

        public bool Register(string login, string password, string name)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(name)) return false;
            if (Core.Context.Users.Any(u => u.Login == login)) return false;

            try
            {
                Core.Context.Users.Add(new Users { Login = login, Password = password, FullName = name, RoleID = 2 });
                Core.Context.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}