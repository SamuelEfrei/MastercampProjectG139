using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139
{
    internal class Medecin : PersonnelSante
    {
        public Medecin(PersonnelSante ps) : base(ps.nom, ps.prenom, ps.numSecuSociale)
        {
            
        }
    }
}
