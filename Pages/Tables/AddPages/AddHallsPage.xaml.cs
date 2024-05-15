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
    public partial class AddHallsPage : Page
    {
        private Halls _currentHall = new Halls();
        public AddHallsPage(Halls selected)
        {
            InitializeComponent();

            if (selected != null )
                _currentHall = selected;

            DataContext = _currentHall;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtTheme.Text))
                errors.AppendLine("Укажите зал!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var hall = MuseumEntities.GetContext().Halls.AsNoTracking().FirstOrDefault(f => f.Theme.ToLower() == txtTheme.Text.ToLower());

            if (hall != null)
            {
                MessageBox.Show("Такой зал уже существует!");
                return;
            }

            _currentHall.Theme = txtTheme.Text;

            if (_currentHall.id == 0)
            {
                MuseumEntities.GetContext().Halls.Add(_currentHall);
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
