using HistoricalMuseum.Pages;
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
    /// Interaction logic for AdminHallsPage.xaml
    /// </summary>
    public partial class DirToursPage : Page
    {
        private string Page = "dir";
        public DirToursPage()
        {
            InitializeComponent();
        }

        private void btnTourProgs_Click(object sender, RoutedEventArgs e)
        {
            TourProgramsPage.GetFromPage(Page);
            NavigationService.Navigate(new TourProgramsPage());
        }

        private void btnHallsInTourProgs_Click(object sender, RoutedEventArgs e)
        {
            HallsInTourProgramsPage.GetFromPage(Page);
            NavigationService.Navigate(new HallsInTourProgramsPage());
        }

        private void btnTourEntries_Click(object sender, RoutedEventArgs e)
        {
            TourEntriesPage.GetFromPage(Page);
            NavigationService.Navigate(new TourEntriesPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DirSectionsPage());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
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

        private void btnExit_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).TextDecorations = TextDecorations.Baseline;
        }

        private void btnExit_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).TextDecorations = TextDecorations.Underline;
        }
    }
}
