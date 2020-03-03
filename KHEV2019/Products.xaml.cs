using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace KHEV2019
{
    /// <summary>
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        public MainWindow mainWindow;
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public SqlDataAdapter adapter;
        public DataSet ds;


        public Products(MainWindow _mainWindow)
        {
            this.setConnection();
            InitializeComponent();
            

            mainWindow = _mainWindow;
            FillList();
        }

        private void setConnection()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void FillList()
        {
            
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string sqlExpression = "sp_ProductsSel";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Products");
                
                adapter.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;              

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


        

        private void add_btn(object sender, RoutedEventArgs e)
        {
            String sql = "sp_Products";
            AUD(sql, 0);
            add.IsEnabled = false;
            Update.IsEnabled = true;
            Delete.IsEnabled = true;

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
                    sql.Parameters.Add("@Id", SqlDbType.Int, 0).Value = Int32.Parse(Books_Id.Text);
                    sql.Parameters.Add("@Product", SqlDbType.NVarChar, 50).Value = Books_name.Text;
                    sql.Parameters.Add("@Cost", SqlDbType.Int, 0).Value = Int32.Parse(Cost.Text);
                    sql.Parameters.Add("@count", SqlDbType.Int, 0).Value = Int32.Parse(Count.Text);
                    break;
                case 1:
                    msg = "Row Updated Successfully";
                    sql.Parameters.Add("@Product", SqlDbType.NVarChar, 50).Value = Books_name.Text;
                    sql.Parameters.Add("@Cost", SqlDbType.Int, 0).Value = Int32.Parse(Cost.Text);
                    sql.Parameters.Add("@count", SqlDbType.Int, 0).Value = Int32.Parse(Count.Text);

                    sql.Parameters.Add("@Id", SqlDbType.Int, 0).Value = Int32.Parse(Books_Id.Text);

                    break;
                case 2:
                    msg = "Row Deleted Succesfully";

                    sql.Parameters.Add("@Id", SqlDbType.Int, 0).Value = Int32.Parse(Books_Id.Text);

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

        private void search_btn(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                if (SrchTxt.Text == "Name")
                {
                    string sqlExpression = "sp_Find_BName";

                    SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Product", FindTxt.Text);
                    adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Products");
                    adapter.Fill(dt);
                    dataGrid1.ItemsSource = dt.DefaultView;
                    adapter.Update(dt);
                }
                else {
                    string sqlExpression1 = "sp_Find_Cost";

                    SqlCommand cmd1 = new SqlCommand(sqlExpression1, sqlCon);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@MinCost", MinCost.Text);
                    cmd1.Parameters.AddWithValue("@MaxCost", MaxCost.Text);
                    adapter = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable("Products");
                    adapter.Fill(dt1);
                    dataGrid1.ItemsSource = dt1.DefaultView;
                    adapter.Update(dt1);
                }

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

        private void Del_btn(object sender, RoutedEventArgs e)
        {
            String sql = "sp_Products_Del";
            AUD(sql, 2);
            Reset_All();
        }

        private void upd_btn(object sender, RoutedEventArgs e)
        {
            String sql = "sp_Products_upd";
            AUD(sql, 1);
        }

        private void res_btn(object sender, RoutedEventArgs e)
        {
            Reset_All();
        }

        private void Reset_All()
        {
            Books_Id.Text = "";
            Books_name.Text = "";
            Cost.Text = "";
            Count.Text = "";

            add.IsEnabled = true;
            Update.IsEnabled = false;
            Delete.IsEnabled = false;
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                Books_Id.Text = dr["id_product"].ToString();
                Books_name.Text = dr["name_product"].ToString();
                Cost.Text = dr["cost"].ToString();
                Count.Text = dr["Count"].ToString();
                Books_Id_1.Text = dr["id_product"].ToString();
                Books_name_1.Text = dr["name_product"].ToString();
                Cost_1.Text = dr["cost"].ToString();
                Count_1.Text = dr["Count"].ToString();
            

                add.IsEnabled = false;
                Update.IsEnabled = true;
                Delete.IsEnabled = true;
            }
        }

        


                   
        

        private void Cost_Click(object sender, RoutedEventArgs e)
        {
            FindTxt.Visibility = Visibility.Hidden;
            From.Visibility = Visibility.Visible;
            To.Visibility = Visibility.Visible;
            MinCost.Visibility = Visibility.Visible;
            MaxCost.Visibility = Visibility.Visible;
            SrchTxt.Text = "Cost";
        }

        private void Name_Click(object sender, RoutedEventArgs e)
        {
            FindTxt.Visibility = Visibility.Visible;
            From.Visibility = Visibility.Hidden;
            To.Visibility = Visibility.Hidden;
            MinCost.Visibility = Visibility.Hidden;
            MaxCost.Visibility = Visibility.Hidden;
            SrchTxt.Text = "Name";
        }

        private void All_View_Click(object sender, RoutedEventArgs e)
        {
            FillList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(mainWindow));
        }
    }
}
