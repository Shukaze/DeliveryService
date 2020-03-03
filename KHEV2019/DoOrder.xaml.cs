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
    /// Логика взаимодействия для DoOrder.xaml
    /// </summary>
    public partial class DoOrder : Page
    {
        public MainWindow mainWindow;
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        
        public string user;

        public DoOrder(string str, MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            user = str;
            UserText.Text = user;
            FillComboAddress();
            TCostd.Text = Basket.strTextChanged;
            DateOrder.SelectedDate = DateTime.Now;
            
            Order_numb();
            
        }

        private void CheckComboPay()
        {
            
            if (ComboPay.SelectedValue.Equals("Card"))
            {
                ComboCard.Visibility = Visibility.Visible;
                Add_Card.Visibility = Visibility.Visible;
                cardLabel.Visibility = Visibility.Visible;
            }
            else
            {
                ComboCard.Visibility = Visibility.Hidden;
                Add_Card.Visibility = Visibility.Hidden;
                cardLabel.Visibility = Visibility.Hidden;
            }
        }
        private void FillComboAddress()
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string sqlExpression = "sp_ComboAddress";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", UserText.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string city = dr.GetString(1);
                    string street = dr.GetString(2);
                    string Number = dr.GetString(3);
                    string address = city + ", " + street + ", " + Number;
                    ComboAddress.Items.Add(address);
                }
                dr.Close();
                string sqlExpression1 = "sp_ComboDelivery";
                SqlCommand cmd1 = new SqlCommand(sqlExpression1, sqlCon);
                cmd1.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    string delivery = dr1.GetString(1);
                    ComboDeliv.Items.Add(delivery);
                }
                dr1.Close();
                string sqlExpression2 = "sp_ComboPayments";
                SqlCommand cmd2 = new SqlCommand(sqlExpression2, sqlCon);
                cmd2.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    string payments = dr2.GetString(1);
                    ComboPay.Items.Add(payments);
                }
                dr2.Close();
                string sqlExpression3 = "sp_ComboCards";
                SqlCommand cmd3 = new SqlCommand(sqlExpression3, sqlCon);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@Username", UserText.Text);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    string card = dr3.GetString(0);
                    ComboCard.Items.Add(card);
                }
                dr3.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                
                sqlCon.Close();
                sqlCon.Dispose();

            }
        }

        private void Order_numb()
        {
            SqlConnection sqlCon = new SqlConnection(ConnectionString);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string sqlExpression = "sp_OrderNumber";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", UserText.Text);
                Order_num.Text = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Click_1(object sender, RoutedEventArgs e)
        {
            
            SqlConnection sql = new SqlConnection(ConnectionString);
            try
            {
                if (sql.State == ConnectionState.Closed)
                    sql.Open();
                
                string sqlExpression3 = "sp_OrderClear";
                SqlCommand cmd3 = new SqlCommand(sqlExpression3, sql);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@Username", UserText.Text);
                cmd3.Parameters.AddWithValue("@DateOrder", DateOrder.SelectedDate);
                cmd3.ExecuteNonQuery();
                
                NavigationService.Navigate(new Basket(UserText.Text, mainWindow));
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sql.Close();
                sql.Dispose();
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (ComboPay.SelectedValue.ToString() == "Card")
            {
                
                SqlConnection sql = new SqlConnection(ConnectionString);
                try
                {                    
                    if (sql.State == ConnectionState.Closed)
                        sql.Open();
                    string sqlExpression = "sp_AddAdditional";
                    SqlCommand cmd = new SqlCommand(sqlExpression, sql);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderNum", Order_num.Text);
                    cmd.Parameters.AddWithValue("@Address", ComboAddress.SelectedValue);
                    cmd.Parameters.AddWithValue("@Delivery", ComboDeliv.SelectedValue);
                    cmd.Parameters.AddWithValue("@Payments", ComboPay.SelectedValue);
                    cmd.Parameters.AddWithValue("@CardNum", ComboCard.SelectedValue);
                    cmd.Parameters.AddWithValue("@DeliveryDate", DeliveryDate.SelectedDate);
                    cmd.Parameters.AddWithValue("@Status", StatusText.Text);
                    cmd.Parameters.AddWithValue("@Username", UserText.Text);
                    cmd.Parameters.AddWithValue("@DateOrder", DateOrder.SelectedDate);
                    cmd.ExecuteNonQuery();
                    String msg3 = "sp_BasketClear";
                    SqlCommand sql3 = new SqlCommand(msg3, sql);
                    sql3.CommandType = CommandType.StoredProcedure;
                    sql3.Parameters.AddWithValue("@Username", UserText.Text);
                    sql3.ExecuteNonQuery();
                    NavigationService.Navigate(new ViewOrderUsers(UserText.Text, mainWindow));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sql.Close();
                    sql.Dispose();
                }
            }
            else
            {
                
                SqlConnection sql = new SqlConnection(ConnectionString);
                try
                {
                    if (sql.State == ConnectionState.Closed)
                        sql.Open();
                    string sqlExpression = "sp_AddAdditionalWithCash";
                    SqlCommand cmd = new SqlCommand(sqlExpression, sql);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderNum", Order_num.Text);
                    cmd.Parameters.AddWithValue("@Address", ComboAddress.SelectedValue);
                    cmd.Parameters.AddWithValue("@Delivery", ComboDeliv.SelectedValue);
                    cmd.Parameters.AddWithValue("@Payments", ComboPay.SelectedValue);
                    cmd.Parameters.AddWithValue("@DeliveryDate", DeliveryDate.SelectedDate);
                    cmd.Parameters.AddWithValue("@Status", StatusText.Text);
                    cmd.Parameters.AddWithValue("@Username", UserText.Text);
                    cmd.Parameters.AddWithValue("@DateOrder", DateOrder.SelectedDate);
                    cmd.ExecuteNonQuery();
                    String msg3 = "sp_BasketClear";
                    SqlCommand sql3 = new SqlCommand(msg3, sql);
                    sql3.CommandType = CommandType.StoredProcedure;
                    sql3.Parameters.AddWithValue("@Username", UserText.Text);
                    sql3.ExecuteNonQuery();
                    MessageBox.Show("Good");
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sql.Close();
                    sql.Dispose();
                }   
            }
        }

        private void Add_Card_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Card(UserText.Text, mainWindow));
        }

       

        private void ComboPay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckComboPay();
        }
    }
}
