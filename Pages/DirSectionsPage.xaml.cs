using HistoricalMuseum.Pages.Tables;
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
    /// Interaction logic for AdminSectionsPage.xaml
    /// </summary>
    public partial class DirSectionsPage : Page
    {
        public DirSectionsPage()
        {
            InitializeComponent();
        }

        private void btnMuseum_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DirMuseumPage());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GuidedToursPage());
        }

        private void Report_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PopularityOfProgramPage());
        }

        private void btnTours_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DirToursPage());
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
