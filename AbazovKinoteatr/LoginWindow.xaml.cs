using System;
using System.Linq;
using System.Windows;
using AbazovKinoteatr.Windows;

namespace AbazovKinoteatr.Windows
{
    public partial class LoginWindow : Window
    {
        private int failedAttempts = 0;
        private string correctCaptcha = "";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            // Проверка капчи, если попыток >= 3
            if (failedAttempts >= 3)
            {
                if (CaptchaInput.Text != correctCaptcha)
                {
                    MessageBox.Show("Неверная капча!");
                    GenerateCaptcha();
                    return;
                }
            }

            if (Auth(LoginBox.Text, PassBox.Password))
            {
                new MainWindow().Show();
                this.Close();
            }
            else
            {
                failedAttempts++;
                if (failedAttempts >= 3)
                {
                    CaptchaStack.Visibility = Visibility.Visible;
                    GenerateCaptcha();
                }
                MessageBox.Show($"Ошибка! Попыток: {failedAttempts}");
            }
        }

        private void GenerateCaptcha()
        {
            Random rnd = new Random();
            correctCaptcha = rnd.Next(1000, 9999).ToString();

            // Теперь это имя будет распознано, так как оно есть в XAML
            CaptchaPlaceholder.Text = correctCaptcha;
            CaptchaInput.Text = "";
        }

        public bool Auth(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password)) return false;
            var user = Core.Context.Users.AsNoTracking().FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user != null)
            {
                Core.CurrentUser = user;
                return true;
            }
            return false;
        }

        private void RegClick(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow().Show();
            this.Close();
        }
    }
}