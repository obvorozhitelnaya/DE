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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DE
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString = "Data Source=localhost;Initial Catalog=DE_1;Integrated Security=True;";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void guestBtn_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = new Guest();
            guest.Show();
            this.Close();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string login = loginTB.Text;
            string password = passTB.Password;
            string sql = $"SELECT * FROM Users WHERE Login='{login}' AND Password='{password}'";
            connection.Open();
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                MessageBox.Show("Пользователь найден");
                reader.Read();
                string role = reader.GetString(0);
                string fullName = reader.GetString(1);
                if(role == "Авторизированный клиент")
                {
                    Client client = new Client();
                    client.fullNameL.Content = fullName;
                    client.Show();
                    this.Close();

                }
                else if(role == "Администратор")
                {
                    Admin admin = new Admin();
                    admin.fullNameL.Content = fullName;
                    admin.Show();
                    this.Close();
                }
                else if(role == "Менеджер")
                {
                    Manager manager = new Manager();
                    manager.fullNameL.Content = fullName;
                    manager.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
