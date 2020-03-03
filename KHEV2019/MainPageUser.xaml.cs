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

namespace KHEV2019
{
    /// <summary>
    /// Логика взаимодействия для MainPageUser.xaml
    /// </summary>
    public partial class MainPageUser : Page
    {
        public MainWindow mainWindow;
        public string user;

        public MainPageUser(string str, MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            user = str;
            UserText.Text = user;
        }


        private void ProductUser_Click_1(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new ProductsUser(UserText.Text, mainWindow));
        }

        private void Basket_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Basket(UserText.Text, mainWindow));
        }

        private void Exit_Click_3(object sender, RoutedEventArgs e)
        {

            mainWindow.OpenPage(MainWindow.pages.LoginPage);

        }

        private void Address_Click_4(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Address(UserText.Text, mainWindow));
        }

        

        private void OrdersUser_Click_5(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewOrderUsers(UserText.Text, mainWindow));
        }
    }
}
