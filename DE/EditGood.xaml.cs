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
    /// Логика взаимодействия для EditGood.xaml
    /// </summary>
    public partial class EditGood : Window
    {
        string connectionString = "Data Source=localhost;Initial Catalog=DE_1;Integrated Security=True;";
        string thisArticle = "";

        public EditGood()
        {
            InitializeComponent();
        }
        public EditGood(string article) : this() // : this() вызывает конструктор по умолчанию
        {
            InitializeComponent();

            thisArticle = article;
        }

        private void editGoodBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = $"UPDATE Goods SET Article = '{articleTB.Text}', Title = '{titleTB.Text}', Unit='{unitTB.Text}', Cost = '{costTB.Text}', Provider = '{provTB.Text}', " +
                $"Producer = '{prodTB.Text}', Category = '{categoryTB.Text}', Discount = '{discountTB.Text}', Amount = '{amountTB.Text}', Description = '{descTB.Text}' WHERE Article ='{thisArticle}';";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
            Admin admin = new Admin();
            admin.Show();
            this.Close();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(thisArticle))
            {
                MessageBox.Show("Товар не выбран!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }

            // ПРЯМО ЗДЕСЬ ЗАГРУЖАЕМ ДАННЫЕ
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT * FROM Goods WHERE Article = '{thisArticle}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    articleTB.Text = reader["Article"].ToString();
                    titleTB.Text = reader["Title"].ToString();
                    unitTB.Text = reader["Unit"].ToString();
                    costTB.Text = reader["Cost"].ToString();
                    provTB.Text = reader["Provider"].ToString();
                    prodTB.Text = reader["Producer"].ToString();
                    categoryTB.Text = reader["Category"].ToString();
                    discountTB.Text = reader["Discount"].ToString();
                    amountTB.Text = reader["Amount"].ToString();
                    descTB.Text = reader["Description"].ToString();
                    if (reader["Photo"].ToString() != "")
                    {
                        imageB.Source = new BitmapImage(new Uri("images/" + reader["Photo"].ToString(), UriKind.Relative));
                    }
                    else
                    {
                        imageB.Source = new BitmapImage(new Uri("images/picture.png", UriKind.Relative));
                    }
                    articleTB.IsReadOnly = true;
                }
                else
                {
                    MessageBox.Show("Товар не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
