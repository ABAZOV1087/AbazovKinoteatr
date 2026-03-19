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
    public partial class MovieDetailsPage : Page
    {
        public Movies SelectedMovie { get; set; }

        public MovieDetailsPage(Movies movie)
        {
            InitializeComponent();
            SelectedMovie = movie;
            DataContext = this;
            // Загружаем сеансы именно для этого фильма
            SessionsList.ItemsSource = Core.Context.Sessions.Where(s => s.MovieID == movie.MovieID).ToList();
        }

        private void SessionSelect(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (SessionsList.SelectedItem is Sessions session)
            {
                // Переход к выбору мест
                NavigationService.Navigate(new SeatSelectionPage(session));
            }
        }

        private void BackClick(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}