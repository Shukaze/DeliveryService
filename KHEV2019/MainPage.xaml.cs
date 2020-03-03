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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public string user;

        public MainWindow mainWindow;
        public Products products;

        public MainPage(MainWindow _mainWindow)
        {
            
            InitializeComponent();
            mainWindow = _mainWindow;
            
            
        }

        private void Product_Click_1(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Products(mainWindow));
        }

        private void Orders_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewOrders(mainWindow));
        }

        private void LoginPage_Click_3(object sender, RoutedEventArgs e)
        {

            mainWindow.OpenPage(MainWindow.pages.LoginPage);

        }

        private void User_Click_4(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AllUser(mainWindow));
        }

        private void XML_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProducts(mainWindow));
        }
    }
}
