using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Configuration;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KHEV2019
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public MainWindow mainWindow;

        public RegPage(MainWindow _mainWindow)
        {
            InitializeComponent();

            mainWindow = _mainWindow;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text.Length > 0) // проверяем логин
            {
                if (P1.Password.Length > 0) // проверяем пароль
                {
                    if (P2.Password.Length > 0) // проверяем второй пароль
                    {
                        if (P1.Password == P2.Password) // проверка на совпадение паролей
                        {
                            SqlConnection sqlCon = new SqlConnection(ConnectionString);
                            {
                                try
                                {                                                                     
                                    string sqlExpression = "sp_User";
                                    if (sqlCon.State == ConnectionState.Closed)
                                        sqlCon.Open();
                                    SqlCommand sqlCmd = new SqlCommand(sqlExpression, sqlCon);
                                    sqlCmd.CommandType = CommandType.StoredProcedure;
                                    sqlCmd.Parameters.AddWithValue("@Username", login.Text);
                                    if (sqlCmd.ExecuteScalar() != null) // если такая запись существует
                                    {
                                        MessageBox.Show("Пользователь уже зарегистрирован."); // говорим, что авторизовался
                                    }
                                    else
                                    {
                                        string strSQL = "sp_RegUsers";
                                        SqlCommand cmd = new SqlCommand(strSQL, sqlCon);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Username", login.Text);
                                        cmd.Parameters.AddWithValue("@Password", P1.Password.GetHashCode().ToString());
                                        cmd.Parameters.AddWithValue("@Role", Role.Text);
                                        cmd.Parameters.AddWithValue("@First_Name", first_name.Text);
                                        cmd.Parameters.AddWithValue("@Last_Name", last_name.Text);
                                        cmd.Parameters.AddWithValue("@Phone_Number", Phone.Text);
                                        cmd.Parameters.AddWithValue("@Address", Address.Text);
                                        cmd.Parameters.AddWithValue("@Email", Email.Text);
                                        if (cmd.ExecuteNonQuery() == 1)
                                            MessageBox.Show("Пользователь зарегистрирован.");
                                    }
                                    sqlCon.Close();
                                }
                                catch (SqlException ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                        else MessageBox.Show("Пароли не совпадают");
                    }
                    else MessageBox.Show("Повторите пароль");
                }
                else MessageBox.Show("Укажите пароль");
            }
            else MessageBox.Show("Укажите логин");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.LoginPage);
        }
    }
}
