using HistoricalMuseum.Pages;
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

namespace HistoricalMuseum
{
    /// <summary>
    /// Interaction logic for HallsPage.xaml
    /// </summary>
    public partial class StaffPage : Page
    {
        public StaffPage()
        {
            InitializeComponent();
            DataGridStaff.ItemsSource = MuseumEntities.GetContext().Staff.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DirMuseumPage());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddStaffPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                MuseumEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridStaff.ItemsSource = MuseumEntities.GetContext().Staff.ToList();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var elemForRemoving = DataGridStaff.SelectedItems.Cast<Staff>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {elemForRemoving.Count()} элементов?", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MuseumEntities.GetContext().Staff.RemoveRange(elemForRemoving);
                    MuseumEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DataGridStaff.ItemsSource = MuseumEntities.GetContext().Staff.ToList();
                }
                catch
                {
                    return;
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddStaffPage((sender as Button).DataContext as Staff));
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
                DataGridStaff.ItemsSource = MuseumEntities.GetContext().Staff.Where(x => x.FIOStaff.Contains(txtSearch.Text) || x.Posts.Post.Contains(txtSearch.Text)).ToList();
            else
            {
                DataGridStaff.ItemsSource = MuseumEntities.GetContext().Staff.ToList();
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
