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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AbazovKinoteatr.Pages 
{
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();

            if (Core.CurrentUser == null)
            {
                MessageBox.Show("Ошибка: пользователь не авторизован!");
                return;
            }
            UserNameTxt.Text = $"Имя: {Core.CurrentUser.FullName}";
            UserLoginTxt.Text = $"Логин: {Core.CurrentUser.Login}";

            TicketsList.ItemsSource = Core.Context.Tickets
                .Where(t => t.UserID == Core.CurrentUser.UserID)
                .OrderByDescending(t => t.PurchaseDate)
                .ToList();
        }

        private void BackClick(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}