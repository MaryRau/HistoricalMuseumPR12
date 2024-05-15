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
    /// Interaction logic for ResearcherSectionsPage.xaml
    /// </summary>
    public partial class ResearcherSectionsPage : Page
    {
        private string Page = "researcher";
        public ResearcherSectionsPage()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void btnExh_Click(object sender, RoutedEventArgs e)
        {
            ExhibitsPage.GetFromPage(Page);
            NavigationService.Navigate(new ExhibitsPage());
        }

        private void btnAuthors_Click(object sender, RoutedEventArgs e)
        {
            AuthorsPage.GetFromPage(Page);
            NavigationService.Navigate(new AuthorsPage());
        }

        private void btnCountries_Click(object sender, RoutedEventArgs e)
        {
            CountriesPage.GetFromPage(Page);
            NavigationService.Navigate(new CountriesPage());
        }

        private void btnEpochs_Click(object sender, RoutedEventArgs e)
        {
            HistoricalEpochsPage.GetFromPage(Page);
            NavigationService.Navigate(new HistoricalEpochsPage());
        }

        private void btnTypes_Click(object sender, RoutedEventArgs e)
        {
            ExhibitsTypesPage.GetFromPage(Page);
            NavigationService.Navigate(new ExhibitsTypesPage());
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
