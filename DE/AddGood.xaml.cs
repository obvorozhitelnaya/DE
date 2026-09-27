using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace DE
{
    /// <summary>
    /// Логика взаимодействия для AddGood.xaml
    /// </summary>
    public partial class AddGood : Window
    {
        string connectionString = "Data Source=localhost;Initial Catalog=DE_1;Integrated Security=True;";

        public AddGood()
        {
            InitializeComponent();
        }

        private void addGoodBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = $"INSERT INTO Goods (Article, Title, Unit, Cost, Provider, Producer, Category, Discount, Amount, Description, Photo) VALUES ('{articleTB.Text}', '{titleTB.Text}', '{unitTB.Text}', '{costTB.Text}', '{provTB.Text}', '{prodTB.Text}', '{categoryTB.Text}', '{discountTB.Text}', '{amountTB.Text}', '{descTB.Text}', '');";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
            Admin admin = new Admin();
            admin.Show();
            this.Close();
        }
    }
}
