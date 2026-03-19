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
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            try
            {
                UpdateList(null, null);
            }
            catch { }

            if (Core.CurrentUser != null)
            {
                LoginBtn.Visibility = Visibility.Collapsed;
                RegBtn.Visibility = Visibility.Collapsed;
                ProfileBtn.Visibility = Visibility.Visible;
            }
        }

        private void UpdateList(object sender, System.EventArgs e) 
        {
            var list = Core.Context.Movies.ToList();
            if (!string.IsNullOrWhiteSpace(SearchBox.Text))
                list = list.Where(p => p.Title.ToLower().Contains(SearchBox.Text.ToLower())).ToList();

            if (SortCombo.SelectedIndex == 1) list = list.OrderBy(p => p.Title).ToList();
            else if (SortCombo.SelectedIndex == 2) list = list.OrderByDescending(p => p.Rating).ToList();

            MoviesList.ItemsSource = list;
        }

        private void ProfileClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfilePage());
        }

        private void MovieSelect(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (MoviesList.SelectedItem is Movies movie)
                NavigationService.Navigate(new MovieDetailsPage(movie));
        }
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            Windows.LoginWindow loginWin = new Windows.LoginWindow();
            loginWin.Show();
            Window.GetWindow(this).Close(); // Закрываем текущее главное окно
        }

        private void RegClick(object sender, RoutedEventArgs e)
        {
            Windows.RegistrationWindow regWin = new Windows.RegistrationWindow();
            regWin.Show();
            Window.GetWindow(this).Close();
        }
    }

}
