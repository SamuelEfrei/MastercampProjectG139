using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139
{
    internal class PersonnelSante
    {
        private string nom;
        private string prenom;
        private int numSS;

        public PersonnelSante(string nom, string prenom, int numSS)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.numSS = numSS;
        }

        public string getNom()
        {
            return nom;
        }

        public string getPrenom()
        {
            return prenom;
        }

        public int getNumSS()
        {
            return numSS;
        }
    }
}
