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
    /// Логика взаимодействия для ProductsUser.xaml
    /// </summary>
    public partial class ProductsUser : Page
    {
        public MainWindow mainWindow;
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public SqlDataAdapter adapter;
        public DataSet ds;
        public string user;
        public int id_produ;


        public ProductsUser(string str, MainWindow _mainWindow)
        {
            this.setConnection();
            InitializeComponent();
            Counts.Text = "0";
            Count_1.Text = "0";
            Counts.TextChanged += Result_TextChanged;
            Count_1.TextChanged += Result_TextChanged;
            user = str;
            
            mainWindow = _mainWindow;
            FillListProduct();
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


        public void FillListProduct()
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
                else
                {
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
     

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                Books_Id_1.Text = dr["ID"].ToString();
                Books_name_1.Text = dr["Name Product"].ToString();
                Cost_1.Text = dr["Cost"].ToString();
                Count_1.Text = dr["Count"].ToString();
                Books_Id_2.Text = dr["ID"].ToString();
            }
            int id1 = CheckIdBook();

            if (id1 == 1)
            {
                AddToBasket.IsEnabled = false;
            }
            else
            {
                AddToBasket.IsEnabled = true;
            }
        }



        public void Add_to_Basket_btn(object sender, RoutedEventArgs e)
        {
            if (Counts.Text != "" || Convert.ToInt32(Counts.Text) > Convert.ToInt32(Count_1.Text) )
            {
                SqlConnection sqlCon = new SqlConnection(ConnectionString);
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string sqlExpression = "sp_UpdateCount";

                    SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@count", Result.Text);
                    cmd.Parameters.AddWithValue("@ID", Books_Id_1.Text);

                    adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Products");
                    adapter.Fill(dt);
                    dataGrid1.ItemsSource = dt.DefaultView;
                    adapter.Update(dt);
                    FillListProduct();

                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string sqlExpression1 = "sp_AddToBasket";
                    SqlCommand cmd1 = new SqlCommand(sqlExpression1, sqlCon);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = user;
                    cmd1.Parameters.Add("@id_prod", SqlDbType.Int, 0).Value = Int32.Parse(Books_Id_1.Text);
                    cmd1.Parameters.Add("@name_product", SqlDbType.NVarChar, 50).Value = Books_name_1.Text;
                    cmd1.Parameters.Add("@cost", SqlDbType.Int, 0).Value = Int32.Parse(Cost_1.Text);
                    cmd1.Parameters.Add("@count_add", SqlDbType.Int, 0).Value = Int32.Parse(Counts.Text);
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("Product added");

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
            else
            {
                MessageBox.Show("Error!");
                Counts.Text = "0";
            }
        }

        private void Result_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
               int a = Int32.Parse(Count_1.Text) - Int32.Parse(Counts.Text);
               if (a >= 0)
               Result.Text = (Int32.Parse(Count_1.Text) - Int32.Parse(Counts.Text)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int CheckIdBook()
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
                String query = "sp_CheckBookID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@id_prod", Books_Id_1.Text);
                sqlCmd.Parameters.AddWithValue("@Username", user);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                id_produ = count;
            return id_produ;
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPageUser(user, mainWindow));
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

        private void Cost_Click(object sender, RoutedEventArgs e)
        {
            FindTxt.Visibility = Visibility.Hidden;
            From.Visibility = Visibility.Visible;
            To.Visibility = Visibility.Visible;
            MinCost.Visibility = Visibility.Visible;
            MaxCost.Visibility = Visibility.Visible;
            SrchTxt.Text = "Cost";
        }

        private void All_View_Click(object sender, RoutedEventArgs e)
        {
            FillListProduct();
        }
    }
}
