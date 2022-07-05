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
using System.Security.Cryptography;
using System.IO;

namespace MastercampProjectG139
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        Config conf;

        public LoginScreen()
        {
            InitializeComponent();
            conf = new Config(); //Charge la configuration
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

            String connectionString = conf.DbConnectionString;
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

        private string Encrypt(string clearText)    //Encryption de texte
        {
            string EncryptionKey = "H179Z0GWNA4JG1J";   //Clé utilisée pour encrypter et décrypter infos avant de les envoyer vers bdd
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);   //Conversion du string en bytes
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());   //Conversion des bytes en string
                }
            }
            return clearText;
        }
        private void SetNumSS(string user) //Fonction pour modifier un numéro de sécurité sociale (numSS)
        {
            string pwd = "$2a$11$jw52E8EggbrAMnbIPL5xV.EKa2RlLvN3x1Ae8e6HPs5KD409cwEua";
            String connectionString = conf.DbConnectionString;
            MySqlConnection connection = new MySqlConnection(connectionString);
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                String query = "UPDATE PersonnelSante SET numSSPersonnel=@parm1 WHERE mdp=@parm2";
                MySqlCommand mySqlCmd = new MySqlCommand(query, connection);
                mySqlCmd.CommandType = System.Data.CommandType.Text;
                mySqlCmd.Parameters.Add("@parm1", MySqlDbType.VarChar);
                mySqlCmd.Parameters.Add("@parm2", MySqlDbType.VarChar);
                mySqlCmd.Parameters["@parm1"].Value = Encrypt(user);
                mySqlCmd.Parameters["@parm2"].Value = pwd;
                mySqlCmd.ExecuteNonQuery();
            }
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)  
        {
            if(txtNumSecu.Text != "" && txtPwd.Password != null)
                loginUser();
        }

        private void login_EnterDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && txtNumSecu.Text != "" && txtPwd.Password != null)
            {
                loginUser();
            }
        }

        //Fonction pour passer de fenêtre login à autre fenêtre quand utilisateur clique sur "connexion" ou sur la touche Entrée
        private void loginUser()
        {
            String connectionString = conf.DbConnectionString; //Paramètres de connexion à la base de données (bdd)
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
                mySqlCmd.Parameters.AddWithValue("@Username", Encrypt(txtNumSecu.Text)); //Identifiant renseigné ici plutôt que dans requête SQL pour éviter injection SQL malveillante

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
                    var idMedecin = (string?)mySqlCmd2.ExecuteScalar(); //récupération d'un seul argument (ici l'ID du personnel soignant). L'argument peut être null si introuvable

                    if (idMedecin != null) //Si le résultat n'est pas null alors c'est un médecin
                    {
                        Medecin medecin = new Medecin(idPS, nom, prenom, Convert.ToInt32(idMedecin));
                        MainWindow vueMedecin = new MainWindow(medecin);
                        vueMedecin.Show();
                    }
                    else //Sinon c'est un pharmacien
                    {
                        //Et dans ce cas on récupère l'ID du pharmacien dans la table pharmacien
                        string query3 = "SELECT idPharma FROM Pharmacien WHERE idPS=@IdPS";
                        MySqlCommand mySqlCmd3 = new MySqlCommand(query3, connection);
                        mySqlCmd3.CommandType = System.Data.CommandType.Text;
                        mySqlCmd3.Parameters.AddWithValue("@IdPS", idPS);
                        var idPharma = (string?)mySqlCmd3.ExecuteScalar();

                        Pharmacien pharmacien = new Pharmacien(idPS, nom, prenom, Convert.ToInt32(idPharma));
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

            //Fonction pour modifier numSS (suite)
            /*try
            {
                SetNumSS(txtNumSecu.Text);
                MainWindow dashboard = new MainWindow();
                dashboard.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }
    }
}
