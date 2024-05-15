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
    public partial class AddCountryPage : Page
    {
        private Countries _currentCountry = new Countries();
        public AddCountryPage( Countries selected)
        {
            InitializeComponent();

            if (selected != null)
                _currentCountry = selected;

            DataContext = _currentCountry;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtCountry.Text))
                errors.AppendLine("Укажите страну!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var country = MuseumEntities.GetContext().Countries.AsNoTracking().FirstOrDefault(f => f.Country.ToLower() == txtCountry.Text.ToLower());

            if (country != null)
            {
                MessageBox.Show("Такая страна уже существует!");
                return;
            }

            _currentCountry.Country = txtCountry.Text;

            if (_currentCountry.id == 0)
            {
                MuseumEntities.GetContext().Countries.Add(_currentCountry);
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
