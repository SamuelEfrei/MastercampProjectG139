using System;
using System.Collections.Generic;
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
using MySql.Data.MySqlClient;

namespace MastercampProjectG139
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void CloseLoginBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoginCard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            String connectionString = "SERVER=localhost;DATABASE=profil;UID=root;PASSWORD=password";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                String query = "SELECT COUNT(1) FROM profil WHERE Username=@Username AND Password=@Password";
                MySqlCommand mySqlCmd = new MySqlCommand(query, connection);
                mySqlCmd.CommandType = System.Data.CommandType.Text;
                mySqlCmd.Parameters.AddWithValue("@Username", txtNumSecu.Text);
                mySqlCmd.Parameters.AddWithValue("@Password", txtPwd.Password);
                int count = Convert.ToInt32(mySqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or Password is ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
