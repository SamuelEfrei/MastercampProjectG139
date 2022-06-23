using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139
{
    internal class PersonnelSante
    {
        private String nom;
        private String prenom;
        private int numSecuSociale;

        public PersonnelSante(String nom, String prenom, int numSecuSociale)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.numSecuSociale = numSecuSociale;
        }
    }
}
