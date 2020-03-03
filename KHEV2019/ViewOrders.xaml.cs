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
    /// Логика взаимодействия для ViewOrders.xaml
    /// </summary>
    public partial class ViewOrders : Page
    {
        public MainWindow mainWindow;
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public SqlDataAdapter adapter;
        public DataSet ds;
        


        public ViewOrders(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            FillListOrder();
        }



        private void FillListOrder()
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string sqlExpression = "sp_ViewOrdersAdmin";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
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
            NavigationService.Navigate(new MainPage(mainWindow));
        }

        private void All_Orders_Click(object sender, RoutedEventArgs e)
        {
            FillListOrder();
        } 

        private void Inactive_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string sqlExpression = "sp_ViewOrdersAdmin";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
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

        private void Active_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string sqlExpression = "sp_ActiveStatus";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
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

        private void Check_Status_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string sqlExpression = "sp_CheckStatus";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
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
                string sqlExpression = "sp_Find_Order";

                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Product", SearchTxt.Text);
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

        
    }
}
