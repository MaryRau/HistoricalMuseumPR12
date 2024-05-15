using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class AddTourEntriesPage : Page
    {
        private TourEntries _currentEntry = new TourEntries();
        private DateTime dateAndTime;
        public AddTourEntriesPage(TourEntries selected)
        {
            InitializeComponent();

            cmbGuide.ItemsSource = MuseumEntities.GetContext().Staff.ToList();
            cmbGuide.DisplayMemberPath = "FIOStaff";
            cmbTourProgram.ItemsSource = MuseumEntities.GetContext().TourPrograms.ToList();
            cmbTourProgram.DisplayMemberPath = "TourTheme";

            if (selected != null)
            {
                _currentEntry = selected;
                cmbGuide.SelectedItem = MuseumEntities.GetContext().Staff.FirstOrDefault(x => x.id == _currentEntry.Guide);
                cmbTourProgram.SelectedItem = MuseumEntities.GetContext().TourPrograms.FirstOrDefault(x => x.id == _currentEntry.TourProgram);
                txtDate.SelectedDate = Convert.ToDateTime(_currentEntry.DateAndTime.ToString().Split(' ')[0]);
                txtTime.Text = _currentEntry.DateAndTime.ToString().Split(' ')[1];
            }

            else
            {
                cmbGuide.Text = "Обязательный";
                cmbTourProgram.Text = "Обязательный";
            }

            DataContext = _currentEntry;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtDate.Text))
                errors.AppendLine("Укажите дату!");
            if (!string.IsNullOrWhiteSpace(txtTime.Text))
            {
                try
                {
                    DateTime date = (DateTime)txtDate.SelectedDate;
                    TimeSpan time = TimeSpan.Parse(txtTime.Text);
                    dateAndTime = date.Date + time;
                }
                catch (Exception)
                {
                    errors.AppendLine("Ошибка ввода времени");
                }
            }
            else errors.AppendLine("Введите время в формате ЧЧ:ММ");

            if (string.IsNullOrWhiteSpace(cmbTourProgram.Text) || cmbTourProgram.Text == "Обязательный")
                errors.AppendLine("Укажите экскурсию!");
            if (string.IsNullOrWhiteSpace(cmbGuide.Text) || cmbGuide.Text == "Обязательный")
                errors.AppendLine("Укажите экскурсовода!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var selectedGuide = (Staff)cmbGuide.SelectedItem;
            var selectedTour = (TourPrograms)cmbTourProgram.SelectedItem;

            int guideId = selectedGuide.id;
            int tourId = selectedTour.id;

            var entries = MuseumEntities.GetContext().TourEntries.AsNoTracking().FirstOrDefault(f => f.Guide == guideId && f.TourProgram == tourId && f.DateAndTime == dateAndTime);

            if (entries != null)
            {
                MessageBox.Show("Такая запись уже существует!");
                return;
            }

            _currentEntry.DateAndTime = dateAndTime;
            _currentEntry.Guide = guideId;
            _currentEntry.TourProgram = tourId;

            if (_currentEntry.id == 0)
            {
                MuseumEntities.GetContext().TourEntries.Add(_currentEntry);
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
