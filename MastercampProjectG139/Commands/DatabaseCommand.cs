using MastercampProjectG139.Models;
using MastercampProjectG139.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139.Commands
{
    internal class DatabaseCommand
    {


      
        public void OrdoSubmit(Medecin medecin, ModelOrdonnance ordonnance, string numSS)
        {
            //Génère un nombre aléatoire à 6 chiffres
            Random rand = new Random();
            int code = rand.Next(0, 999999);
            //Permet de générer des nombres comme 000123
            String scode = code.ToString("000000");

            Config conf = new Config();
            String connectionString = conf.DbConnectionString;
            MySqlConnection connection = new MySqlConnection(connectionString);
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                String query = "INSERT INTO Ordonnance(codeSecret, numSSPatient, idPharma,idMedecin) VALUES(@code,@numSS,@idP,@idM)";

                String query2 = "SELECT idOrdo FROM Ordonnance WHERE idOrdo =(SELECT LAST_INSERT_ID())";
                String query3 = "SELECT idMedic FROM Medicament WHERE nom=@nom";
                String query4 = "INSERT INTO MedicamentOrdonnance VALUES(@idO,@idM,@duree,@qty)";

                //insert dans la db Ordonnance
                MySqlCommand mySqlCmd = new MySqlCommand(query, connection);
                mySqlCmd.CommandType = System.Data.CommandType.Text;
                mySqlCmd.Parameters.AddWithValue("@code", scode);
                mySqlCmd.Parameters.AddWithValue("@numSS", numSS);
                mySqlCmd.Parameters.AddWithValue("@idP", "1");
                mySqlCmd.Parameters.AddWithValue("@idM", medecin.getIdMedecin());
                mySqlCmd.ExecuteNonQuery();

                //récupère l'id de la dernière ordonnance qui a été insert
                MySqlCommand mySqlCommand = new MySqlCommand(query2, connection);
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                int idOrdo = 0;
                if (reader.Read())
                {
                     idOrdo = (int)reader["idOrdo"];
                }
                reader.Close();
                
                
                //insert la liste des médicaments dans la db
                
                foreach(ModelMedicament med in ordonnance.GetAllMedicaments())
                {
                    MySqlCommand mySqlCmd2 = new MySqlCommand(query4, connection);
                    mySqlCmd2.CommandType = System.Data.CommandType.Text;
                    mySqlCmd2.Parameters.AddWithValue("@idO", idOrdo);
                    mySqlCmd2.Parameters.AddWithValue("@idM", med.Id);
                    mySqlCmd2.Parameters.AddWithValue("@duree", med.Duration);
                    mySqlCmd2.Parameters.AddWithValue("@qty", med.Frequence);
                    mySqlCmd2.ExecuteNonQuery();
                }
                
               
 

                
            }
        }
    }
}
