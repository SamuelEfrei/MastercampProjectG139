using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MastercampProjectG139
{
    public class PersonnelSante
    {
        private int idPS;
        private string nom;
        private string prenom;

        public PersonnelSante(int idPS, string nom, string prenom)
        {
            this.idPS = idPS;
            this.nom = nom;
            this.prenom = prenom;
        }

        #region GETTERS
        public string getNom()
        {
            return nom;
        }

        public string getPrenom()
        {
            return prenom;
        }
        #endregion
    }
}
