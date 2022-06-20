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
using BCrypt;

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
            Application.Current.Shutdown(); //Ferme l'application
        }

        private void LoginCard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove(); //Permet de déplacer la fenêtre n'importe où
        }

        private void SetPassword(string user, string userPassword) //Fonction pour modifier un mot de passe (mdp)
        {
            string pwdToHash = userPassword + "^Y8~JJ"; //Ajout d'un string au mdp pour renforcer la sécurité
            string hashToStoreInDatabase = BCrypt.Net.BCrypt.HashPassword(pwdToHash, BCrypt.Net.BCrypt.GenerateSalt()); //Création du mdp hashé

            String connectionString = "SERVER=localhost;DATABASE=mastercamp;UID=root;PASSWORD=password";
            MySqlConnection connection = new MySqlConnection(connectionString);
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                String query = "UPDATE PersonnelSante SET mdp=@parm1 WHERE numSSPersonnel=@parm2";
                MySqlCommand mySqlCmd = new MySqlCommand(query, connection);
                mySqlCmd.CommandType = System.Data.CommandType.Text;
                mySqlCmd.Parameters.Add("@parm1", MySqlDbType.VarChar);
                mySqlCmd.Parameters.Add("@parm2", MySqlDbType.VarChar);
                mySqlCmd.Parameters["@parm1"].Value = hashToStoreInDatabase;
                mySqlCmd.Parameters["@parm2"].Value = user;
                mySqlCmd.ExecuteNonQuery();
            }
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)  //Fonction pour passer de fenêtre login à autre fenêtre quand utilisateur clique sur "connexion"
        {
            //Fonction pour modifier un mdp (suite)
            /*try
            {
                SetPassword(txtNumSecu.Text, txtPwd.Password);
                MainWindow dashboard = new MainWindow();
                dashboard.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }*/

            String connectionString = "SERVER=localhost;DATABASE=mastercamp;UID=root;PASSWORD=password"; //Paramètres de connexion à la base de données (bdd)
            MySqlConnection connection = new MySqlConnection(connectionString);

            String myStoredHash;
            Boolean verified;

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();  //Lancement de la connexion avec la base de données
                String query = "SELECT mdp FROM PersonnelSante WHERE numSSPersonnel=@Username"; //Requête SQL pour trouver le mdp correspondant à l'identifiant
                MySqlCommand mySqlCmd = new MySqlCommand(query, connection);
                mySqlCmd.CommandType = System.Data.CommandType.Text;
                mySqlCmd.Parameters.AddWithValue("@Username", txtNumSecu.Text); //Identifiant renseigné ici plutôt que dans requête SQL pour éviter injection SQL malveillante
                var mySqlResult = mySqlCmd.ExecuteScalar().ToString(); //Récupération du mdp hashé de la bdd en type string

                if (mySqlResult != null)
                {
                    myStoredHash = mySqlResult; 
                }
                else
                {
                    myStoredHash = "";
                }

                verified = BCrypt.Net.BCrypt.Verify(txtPwd.Password + "^Y8~JJ", myStoredHash); //Hashing du mdp (avec le rajout du string) et comparaison avec celui de la bdd

                if (verified == true) //Si bon mdp --> fenêtre login se ferme et autre fenêtre s'ouvre
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else //Si mauvais mdp --> message d'erreur
                {
                    MessageBox.Show("Username or Password is wrong");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
