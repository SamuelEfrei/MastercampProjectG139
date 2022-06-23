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

            //Données du personnel soignant
            int idPS = -1;
            string nom = "";
            string prenom = "";
            string mdp = "";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();  //Lancement de la connexion avec la base de données
                string query = "SELECT idPS, nom, prenom, mdp FROM PersonnelSante WHERE numSSPersonnel=@Username"; //Requête SQL pour trouver le mdp correspondant à l'identifiant
                MySqlCommand mySqlCmd = new MySqlCommand(query, connection);
                
                mySqlCmd.CommandType = System.Data.CommandType.Text;
                mySqlCmd.Parameters.AddWithValue("@Username", txtNumSecu.Text); //Identifiant renseigné ici plutôt que dans requête SQL pour éviter injection SQL malveillante
                //var mySqlResult = mySqlCmd.ExecuteScalar().ToString(); //Récupération du mdp hashé de la bdd en type string

                MySqlDataReader reader = mySqlCmd.ExecuteReader(); //Récupération des arguments demandés
                while (reader.Read())
                {
                    idPS = (int)reader["idPS"];
                    nom = (string)reader["nom"];
                    prenom = (string)reader["prenom"];
                    mdp = (string)reader["mdp"];
                }

                reader.Close(); //On ferme le reader

                if (mdp != null)
                {
                    myStoredHash = mdp; 
                }
                else
                {
                    myStoredHash = "";
                }

                verified = BCrypt.Net.BCrypt.Verify(txtPwd.Password + "^Y8~JJ", myStoredHash); //Hashing du mdp (avec le rajout du string) et comparaison avec celui de la bdd

                if (verified == true) //Si bon mdp --> fenêtre login se ferme et autre fenêtre s'ouvre en se basant si c'est un médecin ou un pharmacien
                {
                    string query2 = "SELECT idMedecin FROM Medecin WHERE idPS=@IdPS"; //On check si l'utilisateur qui se connecte est un médecin
                    MySqlCommand mySqlCmd2 = new MySqlCommand(query2, connection);
                    mySqlCmd2.CommandType = System.Data.CommandType.Text;
                    mySqlCmd2.Parameters.AddWithValue("@IdPS", idPS);
                    var mySqlResult = (int?)mySqlCmd2.ExecuteScalar(); //récupération d'un seul argument (ici l'ID du personnel soignant). L'argument peut être null si introuvable

                    if(mySqlResult != null) //Si le résultat n'est pas null alors c'est un médecin
                    {
                        Medecin medecin = new Medecin(nom, prenom);
                        MainWindow vueMedecin = new MainWindow(medecin);
                        vueMedecin.Show();
                    } else //Sinon c'est un pharmacien
                    {
                        Pharmacien pharmacien = new Pharmacien(nom, prenom);
                        VuePharmacien vuePharmacien = new VuePharmacien(pharmacien);
                        vuePharmacien.Show();
                    }
                    
                    this.Close();
                }
                else //Si mauvais mdp --> message d'erreur
                {
                    MessageBox.Show("Identifiant ou mot de passe incorrect");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
