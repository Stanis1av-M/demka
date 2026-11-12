using demka.AppData;
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

namespace demka.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = LoginBox.Text.Trim();

                if (login.Length == 0)
                {
                    return;
                }

                var u = AppConnect.modeldb.Users.FirstOrDefault(x => x.login == login);
                var r = AppConnect.modeldb.Roles.FirstOrDefault(t => t.id == u.idRole);


                if (login == null)
                {
                    MessageBox.Show("Введите верные данные");
                    return;
                }

                if (u.idRole == 1 || u.idRole == 2)
                {
                    AppFrame.mainFrame.Navigate(new Pages.OrderPage());
                }
                else
                {
                    AppFrame.mainFrame.Navigate(new Pages.ProductPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ошибка {e.Source}");
            }
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.Navigate(new Pages.ProductPage());
        }
    }
}
