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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        string connectionString = "Data Source=localhost;Initial Catalog=DE_1;Integrated Security=True;";
        string thisArticle = "";
        public Admin()
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
                thisArticle = dataRow[0].ToString();
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

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            try
            {
                
                if (goodsDG.SelectedItem != null)
                {
                    DataRowView dataRow = goodsDG.SelectedItem as DataRowView;
                    string article = dataRow[0].ToString();
                    if (article != "")
                    {
                        
                        string sql = $"DELETE FROM Goods WHERE Goods.Article = '{article}';";
                        SqlCommand cmd = new SqlCommand(sql, connection);
                        cmd.ExecuteNonQuery();
                        string sqlNew = "SELECT * FROM Goods;";
                        SqlCommand cmdNew = new SqlCommand(sqlNew, connection);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmdNew);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        goodsDG.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show("Мы не можем удалить этот товар, так как он есть в заказе.");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
            finally
            {
                connection.Close();
            }

        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AddGood addGood = new AddGood();
            addGood.Show();
            this.Close();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(thisArticle))
            {
                // Передаем артикул в окно редактирования
                EditGood editWindow = new EditGood(thisArticle);
                editWindow.Show();
                this.Close(); 
            }
            else
            {
                MessageBox.Show("Выберите товар для редактирования!");
            }
            
        }
    }
}
