using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139
{
    public class Medecin : PersonnelSante
    {
        private int idMedecin;

        public Medecin(int idPS, string nom, string prenom, int idMedecin) : base(idPS, nom, prenom)
        {
            this.idMedecin = idMedecin;
        }

        public int getIdMedecin()
        {
            return idMedecin;
        }

    }
}
