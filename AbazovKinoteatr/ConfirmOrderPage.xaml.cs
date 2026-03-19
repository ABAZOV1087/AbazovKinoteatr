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
    public partial class ConfirmOrderPage : Page
    {
        private Sessions _session;
        private Seats _seat;

        public ConfirmOrderPage(Sessions session, Seats seat)
        {
            InitializeComponent();
            _session = session;
            _seat = seat;

            MovieTxt.Text = $"Фильм: {session.Movies.Title}";
            HallTxt.Text = $"Зал: {session.Halls.HallName}";
            DateTimeTxt.Text = $"Сеанс: {session.SessionDateTime:dd.MM.yyyy HH:mm}";
            SeatTxt.Text = $"Место: Ряд {_seat.RowNumber}, Номер {_seat.SeatNumber}";
            PriceTxt.Text = $"К оплате: {session.Price} руб.";
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            var ticket = new Tickets
            {
                SessionID = _session.SessionID,
                UserID = Core.CurrentUser.UserID,
                SeatID = _seat.SeatID,
                PurchaseDate = DateTime.Now
            };

            Core.Context.Tickets.Add(ticket);
            Core.Context.SaveChanges();

            MessageBox.Show("Билет успешно оформлен!");
            NavigationService.Navigate(new MainPage());
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}