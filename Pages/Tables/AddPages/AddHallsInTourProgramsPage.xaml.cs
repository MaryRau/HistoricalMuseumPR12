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

namespace HistoricalMuseum.Pages.AddToTables
{
    /// <summary>
    /// Interaction logic for AddAuthorPage.xaml
    /// </summary>
    public partial class AddHallsInTourProgramsPage : Page
    {
        private HallsInTourPrograms _current = new HallsInTourPrograms();
        public AddHallsInTourProgramsPage(HallsInTourPrograms selected)
        {
            InitializeComponent();

            cmbHall.ItemsSource = MuseumEntities.GetContext().Halls.ToList();
            cmbHall.DisplayMemberPath = "Theme";
            cmbTourProg.ItemsSource = MuseumEntities.GetContext().TourPrograms.ToList();
            cmbTourProg.DisplayMemberPath = "TourTheme";

            if (selected != null)
            {
                _current = selected;
                cmbHall.SelectedItem = MuseumEntities.GetContext().Halls.FirstOrDefault(x => x.id == _current.Hall);
                cmbTourProg.SelectedItem = MuseumEntities.GetContext().TourPrograms.FirstOrDefault(x => x.id == _current.TourProgram);
            }

            else
            {
                cmbHall.Text = "Обязательный";
                cmbTourProg.Text = "Обязательный";
            }

            DataContext = _current;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(cmbHall.Text) || cmbHall.Text == "Обязательный")
                errors.AppendLine("Укажите зал!");
            if (string.IsNullOrWhiteSpace(cmbTourProg.Text) || cmbTourProg.Text == "Обязательный")
                errors.AppendLine("Укажите экскурсию!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var selectedHall = (Halls)cmbHall.SelectedItem;
            var selectedTour = (TourPrograms)cmbTourProg.SelectedItem;

            int hallId = selectedHall.id;
            int tourId = selectedTour.id;

            var hallsInTours = MuseumEntities.GetContext().HallsInTourPrograms.AsNoTracking().FirstOrDefault(f => f.Hall == hallId && f.TourProgram == tourId);

            if (hallsInTours != null)
            {
                MessageBox.Show("Такая запись уже существует!");
                return;
            }

            _current.TourProgram = tourId;
            _current.Hall = hallId;

            if (_current.id == 0)
            {
                MuseumEntities.GetContext().HallsInTourPrograms.Add(_current);
            }

            try
            {
                MuseumEntities.GetContext().SaveChanges();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btn_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFCFBDAB");
            (sender as Border).BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF7A6653");
        }

        private void btn_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFEEDCCA");
            (sender as Border).BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF98826C");
        }
    }
}
