using System;
using System.Collections.Generic;
using System.Data.Common;
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
    public partial class AddExhibitsPage : Page
    {
        private Exhibits _currentExh = new Exhibits();
        public AddExhibitsPage(Exhibits selected)
        {
            InitializeComponent();

            cmbType.ItemsSource = MuseumEntities.GetContext().ExhibitsTypes.ToList();
            cmbType.DisplayMemberPath = "Type";
            cmbAuthor.ItemsSource = MuseumEntities.GetContext().Authors.ToList();
            cmbAuthor.DisplayMemberPath = "FIOAuthor";
            cmbEpoch.ItemsSource = MuseumEntities.GetContext().HistoricalEpochs.ToList();
            cmbEpoch.DisplayMemberPath = "Epoch";
            cmbCountry.ItemsSource = MuseumEntities.GetContext().Countries.ToList();
            cmbCountry.DisplayMemberPath = "Country";

            if (selected != null)
            {
                _currentExh = selected;

                cmbType.SelectedItem = MuseumEntities.GetContext().ExhibitsTypes.FirstOrDefault(x => x.id == _currentExh.ExhibitType);
                cmbAuthor.SelectedItem = MuseumEntities.GetContext().Authors.FirstOrDefault(x => x.id == _currentExh.Author);
                cmbEpoch.SelectedItem = MuseumEntities.GetContext().HistoricalEpochs.FirstOrDefault(x => x.id == _currentExh.HistoricalEpoch);
                cmbCountry.SelectedItem = MuseumEntities.GetContext().Countries.FirstOrDefault(x => x.id == _currentExh.CreationCountry);
            }

            else
            {
                cmbType.Text = "Обязательный";
                if (cmbAuthor.SelectedItem == null)
                    cmbAuthor.Text = "Необязательный";
                cmbEpoch.Text = "Обязательный";
                cmbCountry.Text = "Обязательный";
            }

            DataContext = _currentExh;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtExh.Text))
                errors.AppendLine("Укажите название экспоната!");
            if (string.IsNullOrWhiteSpace(cmbType.Text) || cmbType.Text == "Обязательный")
                errors.AppendLine("Укажите тип экспоната!");
            if (string.IsNullOrWhiteSpace(cmbEpoch.Text) || cmbEpoch.Text == "Обязательный")
                errors.AppendLine("Укажите историческую эпоху!");
            if (string.IsNullOrWhiteSpace(cmbCountry.Text) || cmbCountry.Text == "Обязательный")
                errors.AppendLine("Укажите место создания!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var selectedType = (ExhibitsTypes)cmbType.SelectedItem;
            var selectedAuthor = (Authors)cmbAuthor.SelectedItem;
            var selectedEpoch = (HistoricalEpochs)cmbEpoch.SelectedItem;
            var selectedCountry = (Countries)cmbCountry.SelectedItem;

            int typeId = selectedType.id;
            int? authorId = selectedAuthor?.id;
            int epochId = selectedEpoch.id;
            int countryId = selectedCountry.id;

            var exhibit = MuseumEntities.GetContext().Exhibits.AsNoTracking().FirstOrDefault(f => f.Exhibit.ToLower() == txtExh.Text.ToLower() && f.ExhibitType == typeId
            && f.Author == authorId && f.HistoricalEpoch == epochId && f.CreationCountry == countryId && f.Description == txtDiscription.Text);

            if (exhibit != null)
            {
                MessageBox.Show("Такой экспонат уже существует!");
                return;
            }

            _currentExh.Exhibit = txtExh.Text;
            _currentExh.ExhibitType = typeId;
            _currentExh.Author = authorId;
            _currentExh.HistoricalEpoch = epochId;
            _currentExh.CreationCountry = countryId;
            _currentExh.Description = txtDiscription.Text;

            if (_currentExh.id == 0)
            {
                MuseumEntities.GetContext().Exhibits.Add(_currentExh);
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
