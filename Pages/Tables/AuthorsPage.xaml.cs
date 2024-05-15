using HistoricalMuseum.Pages.AddToTables;
using System;
using System.Collections;
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
    /// Interaction logic for AuthorsPage.xaml
    /// </summary>
    public partial class AuthorsPage : Page
    {
        private static string fromPage;
        public AuthorsPage()
        {
            InitializeComponent();
            DataGridAuthors.ItemsSource = MuseumEntities.GetContext().Authors.ToList();
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
            NavigationService.Navigate(new AddAuthorPage(null));
        }

        private void btnEdic_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddAuthorPage((sender as Button).DataContext as Authors));
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var elemForRemoving = DataGridAuthors.SelectedItems.Cast<Authors>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {elemForRemoving.Count()} элементов?", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MuseumEntities.GetContext().Authors.RemoveRange(elemForRemoving);
                    MuseumEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DataGridAuthors.ItemsSource = MuseumEntities.GetContext().Authors.ToList();
                }
                catch (Exception ex)
                {
                    // объект не находится в обджект стейт менеджере???
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                MuseumEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridAuthors.ItemsSource = MuseumEntities.GetContext().Authors.ToList();
            }
        }

        public static string GetFromPage(string from)
        {
            fromPage = from;
            return fromPage;
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
                DataGridAuthors.ItemsSource = MuseumEntities.GetContext().Authors.Where(x => x.FIOAuthor.Contains(txtSearch.Text)).ToList();
            else
            {
                DataGridAuthors.ItemsSource = MuseumEntities.GetContext().Authors.ToList();
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
