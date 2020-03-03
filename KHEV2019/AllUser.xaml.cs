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
    /// Логика взаимодействия для AllUser.xaml
    /// </summary>
    public partial class AllUser : Page
    {
        public MainWindow mainWindow;
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["Connection1"].ConnectionString;
        public SqlDataAdapter adapter;

        public AllUser(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            FillListUsers();
            ChangeRole.IsEnabled = false;
        }

        private void FillListUsers()
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string sqlExpression = "sp_ViewUsers";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Users");
                adapter.Fill(dt);
                DataUsers.ItemsSource = dt.DefaultView;

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

        private void DataUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                RoleTxt.Text = dr["Role"].ToString();
                UserTxt.Text = dr["User"].ToString();
            }
        }

        private void Change_Role_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string sqlExpression = "sp_ChangeRole";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Role", RoleTxt.Text);
                cmd.Parameters.AddWithValue("@Username", UserTxt.Text);
                cmd.ExecuteNonQuery();
                FillListUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(mainWindow));
        }

        private void RoleTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RoleTxt.Text == "User" || RoleTxt.Text == "Admin")
            {
                ChangeRole.IsEnabled = true;
            }
            else
                ChangeRole.IsEnabled = false;
        }
    }
}
