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
using System.Windows.Controls.Primitives;

namespace AbazovKinoteatr.Pages
{
    public partial class SeatSelectionPage : Page
    {
        private Sessions _currentSession;
        private Seats _selectedSeat;

        public SeatSelectionPage(Sessions session)
        {
            InitializeComponent();
            _currentSession = session;
            LoadSeats();
        }

        private void LoadSeats()
        {
            // Получаем ID уже купленных мест на этот сеанс
            var occupiedSeatIds = Core.Context.Tickets
                .Where(t => t.SessionID == _currentSession.SessionID)
                .Select(t => t.SeatID)
                .ToList();

            // Берем все места зала, кроме занятых (по ТЗ: возможность убрать все занятые места)
            var availableSeats = Core.Context.Seats
                .Where(s => s.HallID == _currentSession.HallID && !occupiedSeatIds.Contains(s.SeatID))
                .ToList();

            SeatsControl.ItemsSource = availableSeats;
        }

        private void SeatChecked(object sender, RoutedEventArgs e)
        {
            var btn = sender as ToggleButton;
            int seatId = (int)btn.Tag;
            _selectedSeat = Core.Context.Seats.FirstOrDefault(s => s.SeatID == seatId);

            // Снимаем выделение с других кнопок (выбор только одного места)
            foreach (var item in SeatsControl.Items)
            {
                var container = SeatsControl.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                var toggle = VisualTreeHelper.GetChild(container, 0) as ToggleButton;
                if (toggle != null && toggle != btn) toggle.IsChecked = false;
            }
        }

        private void OrderClick(object sender, RoutedEventArgs e)
        {
            if (_selectedSeat == null)
            {
                MessageBox.Show("Выберите место!");
                return;
            }
            NavigationService.Navigate(new ConfirmOrderPage(_currentSession, _selectedSeat));
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}