using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace KHEV2019
{
    /// <summary>
    /// Логика взаимодействия для Card.xaml
    /// </summary>
    public partial class Card : Page
    {

        public MainWindow mainWindow;
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public SqlDataAdapter adapter;
        public DataSet ds;
        public string user;

        public Card(string str, MainWindow _mainWindow)
        {
            InitializeComponent();
            user = str;
            UserTxt.Text = user;
            mainWindow = _mainWindow;
        }

        public class Validator
        {
            public static bool HasErrors(DependencyObject obj)
            {
                foreach (object child in LogicalTreeHelper.GetChildren(obj))
                {
                    TextBox element = child as TextBox;
                    if (element == null) continue;
                    if (Validation.GetHasError(element) || (element.Text.Length == 0))
                    {
                        return true;
                    }
                    HasErrors(element);
                }
                return false;
            }
        }

        private void Add_Card_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            {
                try
                {
                    string sqlExpression = "sp_CheckCard";
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand(sqlExpression, sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@CardNum", CardNum.Text);
                    sqlCmd.Parameters.AddWithValue("@Username", UserTxt.Text);
                    if (sqlCmd.ExecuteScalar() != null) // если такая запись существует
                    {
                        MessageBox.Show("Карта уже зарегестрированна"); 
                    }
                    else
                    {
                        string strSQL = "sp_AddCard";
                        SqlCommand cmd = new SqlCommand(strSQL, sqlCon);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", UserTxt.Text);
                        cmd.Parameters.AddWithValue("@CardNum", CardNum.Text);
                        cmd.Parameters.AddWithValue("@Owner", OwnerTxt.Text);
                        cmd.Parameters.AddWithValue("@MMYY", MMYYTxt.Text.GetHashCode().ToString());
                        cmd.Parameters.AddWithValue("@CVC", CVCTxt.Text.GetHashCode().ToString());
 

                        if (cmd.ExecuteNonQuery() == 1)
                            MessageBox.Show("Карта зарегистрированна.");
                    }
                    sqlCon.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ResetAll()
        {
            CardNum.Text = "";
            OwnerTxt.Text = "";
            MMYYTxt.Text = "";
            CVCTxt.Text = "";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DoOrder(UserTxt.Text, mainWindow));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ResetAll();
        }

        private void CVCTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void MMYYTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MMYYTxt.Text.Length >4)
                MessageBox.Show("Error");
        }
    }
}
