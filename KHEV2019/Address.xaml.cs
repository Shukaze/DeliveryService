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
    /// Логика взаимодействия для Address.xaml
    /// </summary>
    public partial class Address : Page
    {
        public MainWindow mainWindow;
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public SqlDataAdapter adapter;
        public DataSet ds;
        public string user;

        public Address(string str, MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            user = str;
            UserTxt.Text = user;
            FillList();
        }

        public void FillList()
        {

            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string sqlExpression = "sp_AddressFill";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", UserTxt.Text);
                adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Address");

                adapter.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;

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

        private void AUD(String sql_stmt, int state)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            String msg = "";
            SqlCommand sql = new SqlCommand(msg, con);
            sql.CommandText = sql_stmt;
            sql.CommandType = CommandType.StoredProcedure;

            switch (state)
            {
                case 0:
                    msg = "Row Inserted Successfully";
                    sql.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = UserTxt.Text;
                    sql.Parameters.Add("@City", SqlDbType.NVarChar, 20).Value = City.Text;
                    sql.Parameters.Add("@Street", SqlDbType.NVarChar, 30).Value = Street.Text;
                    sql.Parameters.Add("@NumberH", SqlDbType.NVarChar, 10).Value = NumberH.Text;
                    break;

                case 1:
                    msg = "Row Deleted Succesfully";
                    sql.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = UserTxt.Text;
                    sql.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = City.Text;
                    sql.Parameters.Add("@Street", SqlDbType.NVarChar, 0).Value = Street.Text;
                    sql.Parameters.Add("@NumberH", SqlDbType.NVarChar, 0).Value = NumberH.Text;

                    break;
            }
            try
            {
                int n = sql.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show(msg);
                    FillList();
                }
            }
            catch (Exception expe)
            {
                MessageBox.Show(expe.Message);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            String sql = "sp_AddressAdd";
            AUD(sql, 0);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            String sql = "sp_AddressDel";
            AUD(sql, 1);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                City.Text = dr["City"].ToString();
                Street.Text = dr["Street"].ToString();
                NumberH.Text = dr["NumberHouse"].ToString();

            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new MainPageUser(UserTxt.Text, mainWindow));
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
        }

        void ResetAll()
        {
            City.Text = "";
            Street.Text = "";
            NumberH.Text = "";
        }

       
    }
}
