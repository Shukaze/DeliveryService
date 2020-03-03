using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
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
using System.Configuration;
using System.Windows.Shapes;

namespace KHEV2019
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public MainWindow mainWindow;
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public static string ConnectionString1 = ConfigurationManager.ConnectionStrings["Connection1"].ConnectionString;


        public LoginPage(MainWindow _mainWindow)
        {
            InitializeComponent();

            mainWindow = _mainWindow;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string role = RoleCheck();
            
            if ((txtUsername.Text.Length <= 0) || (txtPassword.Password.Length <= 0))
            { MessageBox.Show("Пустые поля. Пожалуйста повторите попытку"); }
            else
            {
                if (role == "Failed")
                {
                    MessageBox.Show("Undefined login or password");
                }
                else
                {
                    if (role == "Admin")
                    {
                        NavigationService.Navigate(new MainPage(mainWindow));
                    }
                    else if (role == "User")
                    {
                        NavigationService.Navigate(new MainPageUser(txtUsername.Text, mainWindow));
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.RegPage);
        }

        string RoleCheck()
        {
            string role = "Failed";
            SqlConnection sql = new SqlConnection(ConnectionString);
            sql.Open();
            string sp = "sp_Role";
            SqlCommand sqlCmd = new SqlCommand(sp, sql);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password.GetHashCode().ToString());
            object result = sqlCmd.ExecuteScalar();
            string kod = Convert.ToString(result);

            if (kod != "")
            {
                role = kod;
            }
            sql.Close();
            return role;
        }

    }
}
