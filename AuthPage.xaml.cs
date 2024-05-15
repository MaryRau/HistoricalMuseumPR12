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
using System.Diagnostics;

namespace HistoricalMuseum
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
            cmbUsername.ItemsSource = MuseumEntities.GetContext().Users.Select(x => x.User).ToList();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cmbUsername.Text) || string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Выберите роль и введите пароль!");
                return;
            }

            using (var db = new MuseumEntities())
            {
                var user = db.Users.AsNoTracking().FirstOrDefault(u => u.User == cmbUsername.Text && u.Password == txtPassword.Password);

                if (user == null)
                {
                    MessageBox.Show("Неверно введены данные!");
                    return;
                }

                switch (user.User)
                {
                    case "Руководитель":
                        NavigationService.Navigate(new DirSectionsPage());
                        break;
                    case "Администратор":
                        NavigationService.Navigate(new AdminSectionsPage());
                        break;
                    case "Научный сотрудник":
                        NavigationService.Navigate(new ResearcherSectionsPage());
                        break;
                    case "Экскурсовод":
                        NavigationService.Navigate(new GuideSectionsPage());
                        break;
                }
            }
        }

        private void btnEnter_MouseEnter(object sender, MouseEventArgs e)
        {
            borderBtn.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFCFBDAB");
            borderBtn.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF7A6653");
        }

        private void btnEnter_MouseLeave(object sender, MouseEventArgs e)
        {
            borderBtn.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFEEDCCA");
            borderBtn.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF98826C");
        }

        private void brdrBtnHelp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("Разделы.chm");
        }
    }
}
