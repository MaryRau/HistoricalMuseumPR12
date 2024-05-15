using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class AddEpochPage : Page
    {
        private HistoricalEpochs _currentEpoch = new HistoricalEpochs();
        public AddEpochPage(HistoricalEpochs selected)
        {
            InitializeComponent();

            if (selected != null)
                _currentEpoch = selected;

            DataContext = _currentEpoch;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtEpoch.Text))
                errors.AppendLine("Укажите историческую эпоху!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var epoch = MuseumEntities.GetContext().HistoricalEpochs.AsNoTracking().FirstOrDefault(f => f.Epoch.ToLower() == txtEpoch.Text.ToLower());

            if (epoch != null)
            {
                MessageBox.Show("Такая эпоха уже существует!");
                return;
            }

            _currentEpoch.Epoch = txtEpoch.Text;

            if (_currentEpoch.id == 0)
            {
                MuseumEntities.GetContext().HistoricalEpochs.Add(_currentEpoch);
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
