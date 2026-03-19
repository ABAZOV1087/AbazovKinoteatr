using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            var user = new Users { Login = LoginBox.Text, Password = PassBox.Password, FullName = NameBox.Text, RoleID = 1 };
            Core.Context.Users.Add(user);
            Core.Context.SaveChanges();
            MessageBox.Show("Успех!");
            new LoginWindow().Show();
            this.Close();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }
    }
}
