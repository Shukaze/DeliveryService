using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
    /// Логика взаимодействия для ViewOrderUsers.xaml
    /// </summary>
    public partial class ViewOrderUsers : Page
    {

        public MainWindow mainWindow;
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public SqlDataAdapter adapter;
        public DataSet ds;
        public string user;

        public ViewOrderUsers(string str, MainWindow _mainWindow)
        {

            InitializeComponent();
            mainWindow = _mainWindow;
            
            user = str;
            FillListOrder();
        }



        private void FillListOrder()
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string sqlExpression = "sp_ViewOrders";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", user);
                adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("Orders");
                adapter.Fill(dt);
                DataOrder.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                adapter.Dispose();
                sqlCon.Close();
                sqlCon.Dispose();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPageUser(user, mainWindow));
        }
        

        private void Check_Status_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string sqlExpression = "sp_ConfirmStatus";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", user);
                cmd.ExecuteNonQuery();
                FillListOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                string sqlExpression = "sp_Find_Order_Product";

                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Product", SearchTxt.Text);
                cmd.Parameters.AddWithValue("@Username", user);
                adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Orders");
                adapter.Fill(dt);
                DataOrder.ItemsSource = dt.DefaultView;
                adapter.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                adapter.Dispose();
                sqlCon.Close();
                sqlCon.Dispose();

            }
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://localhost/Reports/report/Information");
        }
    }
}
