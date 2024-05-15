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
    public partial class AddExhInStoreroomPage : Page
    {
        private ExhibitsInStoreroom _current = new ExhibitsInStoreroom();
        public AddExhInStoreroomPage(ExhibitsInStoreroom selected)
        {
            InitializeComponent();

            var exhibitsInHallsIds = MuseumEntities.GetContext().ExhibitsInHalls.Select(x => x.Exhibit).ToList();
            var exhibitsInStoreIds = MuseumEntities.GetContext().ExhibitsInStoreroom.Select(x => x.Exhibit).ToList();

            int length = MuseumEntities.GetContext().Exhibits
                .Where(x => !exhibitsInHallsIds.Contains(x.id) && !exhibitsInStoreIds.Contains(x.id))
                .ToList().Count();

            cmbExh.ItemsSource = MuseumEntities.GetContext().Exhibits
                .Where(x => !exhibitsInHallsIds.Contains(x.id) && !exhibitsInStoreIds.Contains(x.id))
                .ToList();
            cmbExh.DisplayMemberPath = "Exhibit";

            if (selected != null)
            {
                _current = selected;
                cmbExh.SelectedItem = MuseumEntities.GetContext().Exhibits.FirstOrDefault(x => x.id == _current.Exhibit);
            }

            else
            {
                if (length == 0)
                {
                    cmbExh.Text = "Невозможно разместить ни один экспонат!";
                    borderBtnSave.Visibility = Visibility.Hidden;
                }
                else cmbExh.Text = "Обязательный";
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

            if (string.IsNullOrWhiteSpace(cmbExh.Text) || cmbExh.Text == "Обязательный")
                errors.AppendLine("Укажите экспонат!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var selectedExh = (Exhibits)cmbExh.SelectedItem;
            int exhId = selectedExh.id;

            var exhInStore = MuseumEntities.GetContext().ExhibitsInStoreroom.AsNoTracking().FirstOrDefault(f => f.Exhibit == exhId);

            if (exhInStore != null)
            {
                MessageBox.Show("Такая запись уже существует!");
                return;
            }

            _current.Exhibit = exhId;

            if (_current.id == 0)
            {
                MuseumEntities.GetContext().ExhibitsInStoreroom.Add(_current);
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
