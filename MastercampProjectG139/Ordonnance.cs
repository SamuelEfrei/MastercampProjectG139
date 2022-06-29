using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Windows;

namespace MastercampProjectG139
{
    public class Ordonnance
    {
        private int idOrdo;
        private int codeSecret;
        private string numSSPatient;
        private int idPharma;
        private int idMedecin;

        public Ordonnance(int idOrdo, int codeSecret, string numSSPatient, int idPharma, int idMedecin)
        {
            this.idOrdo = idOrdo;
            this.codeSecret = codeSecret;
            this.numSSPatient = numSSPatient;
            this.idPharma = idPharma;
            this.idMedecin = idMedecin;
        }

        #region GETTERS
        public int getIdOrdo()
        {
            return idOrdo;
        }

        public int getCodeSecret()
        {
            return codeSecret;
        }

        public string getNumSSPatient()
        {
            return numSSPatient;
        }

        public int getIdPharma()
        {
            return idPharma;
        }

        public int getIdMedecin()
        {
            return idMedecin;
        }
        #endregion

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

        private string Decrypt(string cipherText)   //Décryption de texte
        {
            string EncryptionKey = "H179Z0GWNA4JG1J";   //Clé utilisée pour encrypter et décrypter infos avant de les envoyer vers bdd
            byte[] cipherBytes = Convert.FromBase64String(cipherText);  //Conversion du string crypté en bytes
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());  //Conversion des bytes en string
                }
            }
            return cipherText;
        }

        protected void ordonnanceSubmit(object sender, EventArgs e)   //Envoi de données cryptées vers la bdd
        {
            String connectionString = "SERVER=localhost;DATABASE=mastercamp;UID=root;PASSWORD=password";
            MySqlConnection connection = new MySqlConnection(connectionString);
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                String query = "INSERT INTO Ordonnance VALUES(@idOrdo,@code,@numSS,@idP,@idM)";
                MySqlCommand mySqlCmd = new MySqlCommand(query, connection);
                mySqlCmd.CommandType = System.Data.CommandType.Text;
                mySqlCmd.Parameters.AddWithValue("@idOrdo", Encrypt(getIdOrdo().ToString()));   //Integer transformés en string pour l'encryption
                mySqlCmd.Parameters.AddWithValue("@code", Encrypt(getCodeSecret().ToString()));
                mySqlCmd.Parameters.AddWithValue("@numSS", Encrypt(getNumSSPatient()));
                mySqlCmd.Parameters.AddWithValue("@idP", Encrypt(getIdPharma().ToString()));
                mySqlCmd.Parameters.AddWithValue("@idM", Encrypt(getIdMedecin().ToString()));
                mySqlCmd.ExecuteNonQuery();
            }
        }

        protected MedicamentOrdonnance getMedicamentOrdonnance(object sender, EventArgs e)   //réception de données cryptées de la bdd
        {
            int idOrdo = 0;
            int idMedic = 0;
            string nom = "";
            bool dangereux = false;
            string dureeMedicament = "";
            string quantiteParJour = "";

            string idOrdo_crypt;
            string idMedic_crypt;
            string nom_crypt;
            string dangereux_crypt;
            string dureeMedicament_crypt;
            string quantiteParJour_crypt;

            String connectionString = "SERVER=localhost;DATABASE=mastercamp;UID=root;PASSWORD=password";
            MySqlConnection connection = new MySqlConnection(connectionString);
            {
                try
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    String query = "select o.idOrdo, m.idMedic, m.nom, m.dangereux, mo.dureeMedicament, mo.quantiteParJour from ordonnance o " +
                        "join medicamentordonnance as mo on mo.idOrdo = o.idOrdo" +
                        "join medicament as m on m.idMedic = mo.idMedic" +
                        "where o.codeSecret = @code and numSSPatient = @numSS";
                    MySqlCommand mySqlCmd = new MySqlCommand(query, connection);
                    mySqlCmd.CommandType = System.Data.CommandType.Text;
                    mySqlCmd.Parameters.AddWithValue("@code", Encrypt(getCodeSecret().ToString()));
                    mySqlCmd.Parameters.AddWithValue("@numSS", Encrypt(getNumSSPatient()));


                    MySqlDataReader reader = mySqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        idOrdo_crypt = (string)reader["idOrdo"];    //Récupération des infos cryptées
                        idMedic_crypt = (string)reader["idMedic"];
                        nom_crypt = (string)reader["nom"];
                        dangereux_crypt = (string)reader["dangereux"];
                        dureeMedicament_crypt = (string)reader["dureeMedicament"];
                        quantiteParJour_crypt = (string)reader["quantiteParJour"];

                        idOrdo = int.Parse(Decrypt(idOrdo_crypt));  //Décryptage et conversion de certains string en integer ou boolean
                        idMedic = int.Parse(Decrypt(idMedic_crypt));
                        nom = Decrypt(nom_crypt);
                        dangereux = bool.Parse(Decrypt(dangereux_crypt));
                        dureeMedicament = Decrypt(dureeMedicament_crypt);
                        quantiteParJour = Decrypt(quantiteParJour_crypt);

                    }
                    reader.Close(); //On ferme le reader
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                MedicamentOrdonnance mo = new(idMedic, idOrdo, dureeMedicament, quantiteParJour, nom, dangereux);
                return mo;
            }
        }
    }
}
