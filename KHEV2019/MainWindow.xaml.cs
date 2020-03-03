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
using System.Windows.Shapes;
using System.Configuration;

namespace KHEV2019
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            OpenPage(pages.LoginPage);
        }

        public enum pages
        {
            LoginPage,
            RegPage,
            
        }

        public void OpenPage(pages pages)
        {
            if (pages == pages.LoginPage)
            {
                frame.Navigate(new LoginPage(this));
            }
            else
            if (pages == pages.RegPage)
            {
                frame.Navigate(new RegPage(this));
            }
        }
    }
}
