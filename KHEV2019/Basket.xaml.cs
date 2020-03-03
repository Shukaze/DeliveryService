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
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Page
    {
        public MainWindow mainWindow;
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public SqlDataAdapter adapter;
        public DataSet ds;
        public string user;
        public static string strTextChanged { get; set; }

        public Basket(string str, MainWindow _mainWindow)
        {
            InitializeComponent();
            user = str;
            mainWindow = _mainWindow;
            User.Text = user;
            FillList();
            Total_Cost();
            DateOrder.SelectedDate = DateTime.Now;
            DataGrid2.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPageUser(User.Text, mainWindow));
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            String msg1 = "sp_UpdBasketDate";
            SqlCommand sql1 = new SqlCommand(msg1, con);
            sql1.CommandType = CommandType.StoredProcedure;
            sql1.Parameters.AddWithValue("@DateOrder", DateOrder.SelectedDate);
            sql1.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = User.Text;
            sql1.ExecuteNonQuery();
            String msg = "sp_Orders";
            SqlCommand sql = new SqlCommand(msg, con);
            sql.CommandType = CommandType.StoredProcedure;
            sql.ExecuteNonQuery();
            String msg2 = "sp_UpdOrderCost";
            SqlCommand sql2 = new SqlCommand(msg2, con);
            sql2.CommandType = CommandType.StoredProcedure;
            sql2.Parameters.AddWithValue("@DateOrder", DateOrder.SelectedDate);
            sql2.Parameters.AddWithValue("@TCost", TCost.Text);
            sql2.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = User.Text;
            sql2.ExecuteNonQuery();
            NavigationService.Navigate(new DoOrder(User.Text, mainWindow));
        }

        private void Total_Cost()
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string sqlExpression = "sp_Tcost";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", User.Text);
                TCost.Text = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillList()
        { 
        SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string sqlExpression = "sp_Basket";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", User.Text);
                adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Basket");

                adapter.Fill(dt);
                DataGrid2.ItemsSource = dt.DefaultView;
                
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string sqlExpression = "sp_DeleteBasketRow";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_prod", id_prod.Text);
                cmd.Parameters.AddWithValue("@Username", User.Text);
                cmd.ExecuteNonQuery();
                FillList();
                

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

        private void TCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            Basket.strTextChanged = TCost.Text;
        }

        private void DataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                id_prod.Text = dr["id"].ToString();
            }
        }
    }
}
