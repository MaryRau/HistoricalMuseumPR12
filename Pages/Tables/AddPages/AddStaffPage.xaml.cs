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

namespace HistoricalMuseum.Pages.AddToTables
{
    /// <summary>
    /// Interaction logic for AddAuthorPage.xaml
    /// </summary>
    public partial class AddStaffPage : Page
    {
        private Staff _currentStaff = new Staff();
        public AddStaffPage(Staff selected)
        {
            InitializeComponent();

            cmbPost.ItemsSource = MuseumEntities.GetContext().Posts.ToList();
            cmbPost.DisplayMemberPath = "Post";

            if (selected != null)
            {
                _currentStaff = selected;
                cmbPost.SelectedItem = MuseumEntities.GetContext().Posts.FirstOrDefault(x => x.id == _currentStaff.Post);
            }

            else
            {
                cmbPost.Text = "Обязательный";
            }

            DataContext = _currentStaff;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtStaff.Text))
                errors.AppendLine("Укажите сотрудника!");
            if (string.IsNullOrWhiteSpace(cmbPost.Text) || cmbPost.Text == "Обязательный")
                errors.AppendLine("Укажите должность!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var selectedPost = (Posts)cmbPost.SelectedItem;
            int postId = selectedPost.id;

            var staff = MuseumEntities.GetContext().Staff.AsNoTracking().FirstOrDefault(f => f.FIOStaff == txtStaff.Text && f.Post == postId);

            if (staff != null)
            {
                MessageBox.Show("Такой сотрудник уже существует!");
                return;
            }

            _currentStaff.FIOStaff = txtStaff.Text;
            _currentStaff.Post = postId;

            if (_currentStaff.id == 0)
            {
                MuseumEntities.GetContext().Staff.Add(_currentStaff);
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
