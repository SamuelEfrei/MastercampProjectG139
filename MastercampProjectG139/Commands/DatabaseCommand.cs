using MastercampProjectG139.Models;
using MastercampProjectG139.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MastercampProjectG139.Commands
{
    internal class DatabaseCommand
    {
        private readonly ModelOrdonnance _ordoP;
        public void OrdoSubmit(Medecin medecin, ModelOrdonnance ordonnance, string numSS)
        {
            //Génère un nombre aléatoire à 6 chiffres
            int code = ordonnance.getCode();
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
              
                String query3 = "INSERT INTO MedicamentOrdonnance VALUES(@idO,@idM,@duree,@qty)";

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
                    MySqlCommand mySqlCmd2 = new MySqlCommand(query3, connection);
                    mySqlCmd2.CommandType = System.Data.CommandType.Text;
                    mySqlCmd2.Parameters.AddWithValue("@idO", idOrdo);
                    mySqlCmd2.Parameters.AddWithValue("@idM", med.Id);
                    mySqlCmd2.Parameters.AddWithValue("@duree", med.Duration);
                    mySqlCmd2.Parameters.AddWithValue("@qty", med.Frequence);
                    mySqlCmd2.ExecuteNonQuery();
                }
                
               
 

                
            }
        }

        public void getOrdonnance(Pharmacien pharmacien, string numSS, string code, ModelOrdonnance _ordoP)
        {
            long numdb = -1;
            int codedb = -1;
            int idOrdo = -1;
            Config conf = new Config();
            String connectionString = conf.DbConnectionString;
            //MessageBox.Show(numSS + " " + code, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            MySqlConnection connection = new MySqlConnection(connectionString);
            {
                
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    String query = "Select * from Ordonnance where codeSecret = @code AND numSSPatient = @numSS";
                    MySqlCommand mySqlCmd = new MySqlCommand(query, connection);
                    mySqlCmd.CommandType = System.Data.CommandType.Text;
                    mySqlCmd.Parameters.AddWithValue("@code", code);
                    mySqlCmd.Parameters.AddWithValue("@numSS", numSS);
                    MySqlDataReader reader = mySqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        codedb = (int)reader["codeSecret"];
                        numdb = (long)reader["numSSPatient"];
                        idOrdo = (int)reader["idOrdo"];
                        
                    }
                    String query2 = "Select mo.*, m.nom from MedicamentOrdonnance mo JOIN Medicament m on mo.idMedic = m.idMedic where mo.idOrdo = @idO";
                    reader.Close();
                    if(codedb == -1 || numdb == -1)
                    {
                        MessageBox.Show("Vous n'avez pas rentré les bons", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Ordonnance Récupérée", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        MySqlCommand mySqlCmd2 = new MySqlCommand(query2, connection);
                        mySqlCmd2.CommandType = System.Data.CommandType.Text;
                        mySqlCmd2.Parameters.AddWithValue("@idO", idOrdo);
                        MySqlDataReader reader2 = mySqlCmd2.ExecuteReader();
                            
                          
                        while (reader2.Read())
                    {
                        //MessageBox a enlever lorsque la liste sera terminé, juste ici pour les tests
                        //MessageBox.Show((string)reader2["nom"], "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ModelMedicament medicament = new ModelMedicament((int)reader2["idMedic"], (string)reader2["nom"], (string)reader2["quantiteParJour"] , (string)reader2["dureeMedicament"]);
                        _ordoP.AddMed(medicament);
                    }
                    reader2.Close();
                }
                    

                //catch(Exception e)
                //{
                //    MessageBox.Show("Vous n'avez pas rentrer les bons identifiantsa", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
            }
        }
    }
}
