using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139
{
    public class PersonnelSante
    {
        private string nom;
        private string prenom;

        public PersonnelSante(string nom, string prenom)
        {
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
