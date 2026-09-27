using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace DE
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        string connectionString = "Data Source=localhost;Initial Catalog=DE_1;Integrated Security=True;";

        public Client()
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "SELECT * FROM Goods;";
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            goodsDG.ItemsSource = dt.DefaultView;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void goodsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (goodsDG.SelectedItem != null)
            {
                DataRowView dataRow = goodsDG.SelectedItem as DataRowView;
                categoryL.Content = dataRow[6].ToString();
                titleL.Content = dataRow[1].ToString();
                descL.Content = dataRow[9].ToString();
                prodL.Content = dataRow[5].ToString();
                provL.Content = dataRow[4].ToString();
                costL.Content = dataRow[3].ToString();
                unitL.Content = dataRow[2].ToString();
                amountL.Content = dataRow[8].ToString();
                discountL.Content = dataRow[7].ToString() + "%";
                if (dataRow[10].ToString() != "")
                {
                    imageB.Source = new BitmapImage(new Uri("images/" + dataRow[10].ToString(), UriKind.Relative));
                }
                else
                {
                    imageB.Source = new BitmapImage(new Uri("images/picture.png", UriKind.Relative));
                }

            }
        }
    }
}
