using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139
{
    public class Medecin : PersonnelSante
    {
        public Medecin(string nom, string prenom, int numSecuSociale) : base(nom, prenom, numSecuSociale)
        {
            
        }
    }
}
