using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        string connectionString = "Data Source=localhost;Initial Catalog=DE_1;Integrated Security=True;";

        public Manager()
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
            //заполнение карточки товара
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

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string sql = $"SELECT * FROM Goods WHERE Goods.Article LIKE '%{searchTB.Text}%';";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            goodsDG.ItemsSource = dt.DefaultView;

        }

        private void sortBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = $"SELECT * FROM Goods WHERE Goods.Producer = '{sortedCB.Text}';";
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            goodsDG.ItemsSource = dataTable.DefaultView;
        }

        private void sortedCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void filterBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            if(filterCB.Text == "По возрастанию товара на складе")
            {
                string sql = "SELECT * FROM Goods ORDER BY Goods.Amount ASC;";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                goodsDG.ItemsSource = dataTable.DefaultView;


            }
            else if (filterCB.Text == "По убыванию товара на складе")
            {
                string sql = "SELECT * FROM Goods ORDER BY Goods.Amount DESC;";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                goodsDG.ItemsSource = dataTable.DefaultView;
            }
        }
    }
}
