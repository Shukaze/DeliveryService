using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
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
    

    [Serializable]
    public class Productss
    {
        public List<Productss> ProdList { get; set; } = new List<Productss>();
    }

    [Serializable]
    public class Prod
    {
        public int id_product { get; set; }
        public string name_product { get; set; }
        public int cost { get; set; }
        public int count { get; set; }

        public Prod()
        {
            //without parameters
        }
        public Prod(int ID, string Name, int Cost, int Count)
        {
            id_product = id_product;
            name_product = name_product;
            cost = cost;
            count = count;
        }
    }

    
    public partial class AddProducts : Page
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["Connection1"].ConnectionString;
        public MainWindow mainWindow;
        public SqlDataAdapter adapter;
        public DataTable dt = new DataTable("Products");
        public AddProducts(MainWindow _mainWindow)
        {
            InitializeComponent();

            mainWindow = _mainWindow;
        }
        //Сериализация
        public void Serealization_clk(object sender, RoutedEventArgs e)
        {
            XmlSerializer xml = new XmlSerializer(typeof(DataTable));
            if (File.Exists("./Xmlfile.xml"))
              {
                File.Delete("./Xmlfile.xml");
            }
            using (FileStream fs = new FileStream("./Xmlfile.xml", FileMode.OpenOrCreate))
            {
                SqlConnection sqlCon = new SqlConnection(ConnectionString);
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string sqlExpression = "sp_ProductsSel";
                    SqlCommand cmd = new SqlCommand(sqlExpression, sqlCon);
                    adapter = new SqlDataAdapter(cmd);
                    
                    adapter.Fill(dt);
                    xml.Serialize(fs, dt);
                    MessageBox.Show("Elements are serialized!");
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

        //Десериализация
        private void Deser(object sender, RoutedEventArgs e)
        {
            string path = @".\Xmlfile.xml";
            using (StreamReader fs = new StreamReader(path))
            {
                DataTable Des = new DataTable();
                Des.Clear();
                XmlSerializer xml = new XmlSerializer(typeof(DataTable));
                Des = (DataTable)xml.Deserialize(fs);
                dataGrid2.ItemsSource = Des.DefaultView;

                IList rows = dataGrid2.Items;
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();
                foreach (DataRow dr in Des.Rows)
                {
                    var cells = dr.ItemArray;
                    int ID = Convert.ToInt32(cells[0].ToString());
                    string Name = cells[1].ToString();
                    int Cost = Convert.ToInt32(cells[2].ToString());
                    int Count = Convert.ToInt32(cells[3].ToString());
                    string sqlExp = "sp_XmlDes";
                    SqlCommand comm = new SqlCommand(sqlExp, conn);
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@id_product", ID);
                    comm.Parameters.AddWithValue("@name_product", Name);
                    comm.Parameters.AddWithValue("@cost", Cost);
                    comm.Parameters.AddWithValue("@count", Count);
                    SqlDataAdapter adapter1 = new SqlDataAdapter(comm);
                    adapter1.Fill(dt);
                      
                }
            }      
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(mainWindow));
        }


    }
}
