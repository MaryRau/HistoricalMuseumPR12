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
    /// Interaction logic for GuideSectionsPage.xaml
    /// </summary>
    public partial class TourEntriesPage : Page
    {
        private static string fromPage;

        public TourEntriesPage()
        {
            InitializeComponent();
            DataGridEntries.ItemsSource = MuseumEntities.GetContext().TourEntries.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (fromPage == "guide")
                NavigationService.Navigate(new GuideSectionsPage());
            else if (fromPage == "dir")
                NavigationService.Navigate(new DirToursPage());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddTourEntriesPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                MuseumEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridEntries.ItemsSource = MuseumEntities.GetContext().TourEntries.ToList();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var elemForRemoving = DataGridEntries.SelectedItems.Cast<TourEntries>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {elemForRemoving.Count()} элементов?", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MuseumEntities.GetContext().TourEntries.RemoveRange(elemForRemoving);
                    MuseumEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DataGridEntries.ItemsSource = MuseumEntities.GetContext().TourEntries.ToList();
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
            NavigationService.Navigate(new AddTourEntriesPage((sender as Button).DataContext as TourEntries));
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
                DataGridEntries.ItemsSource = MuseumEntities.GetContext().TourEntries.Where(x => x.TourPrograms.TourTheme.Contains(txtSearch.Text) || x.Staff.FIOStaff.Contains(txtSearch.Text) || x.DateAndTime.ToString().Contains(txtSearch.Text)).ToList();
            else
            {
                DataGridEntries.ItemsSource = MuseumEntities.GetContext().TourEntries.ToList();
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
