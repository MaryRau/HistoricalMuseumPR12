using HistoricalMuseum.Pages.AddToTables;
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

namespace HistoricalMuseum.Pages
{
    /// <summary>
    /// Interaction logic for HistoricalEpochsPage.xaml
    /// </summary>
    public partial class HistoricalEpochsPage : Page
    {
        private static string fromPage;

        public HistoricalEpochsPage()
        {
            InitializeComponent();
            DataGridEpochs.ItemsSource = MuseumEntities.GetContext().HistoricalEpochs.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (fromPage == "researcher")
                NavigationService.Navigate(new ResearcherSectionsPage());
            else if (fromPage == "dir")
                NavigationService.Navigate(new DirMuseumPage());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEpochPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                MuseumEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridEpochs.ItemsSource = MuseumEntities.GetContext().HistoricalEpochs.ToList();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var elemForRemoving = DataGridEpochs.SelectedItems.Cast<HistoricalEpochs>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {elemForRemoving.Count()} элементов?", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MuseumEntities.GetContext().HistoricalEpochs.RemoveRange(elemForRemoving);
                    MuseumEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DataGridEpochs.ItemsSource = MuseumEntities.GetContext().HistoricalEpochs.ToList();
                }
                catch
                {
                    return;
                }
            }
        }

        public static string GetFromPage(string from)
        {
            fromPage = from;
            return fromPage;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEpochPage((sender as Button).DataContext as HistoricalEpochs));
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Поиск")
            {
                txtSearch.Clear();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string s = txtSearch.Text.Trim();
            if (txtSearch.Text != "Поиск" || !string.IsNullOrWhiteSpace(s))
                DataGridEpochs.ItemsSource = MuseumEntities.GetContext().HistoricalEpochs.Where(x => x.Epoch.Contains(txtSearch.Text)).ToList();
            else
            {
                DataGridEpochs.ItemsSource = MuseumEntities.GetContext().HistoricalEpochs.ToList();
                txtSearch.Text = "Поиск";
            }
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
                txtSearch.Text = "Поиск";
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

        private void Viewbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Поиск")
                txtSearch.Text = "";
        }
    }
}
