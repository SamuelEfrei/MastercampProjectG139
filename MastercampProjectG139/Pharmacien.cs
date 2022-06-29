using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139
{
    public class Pharmacien : PersonnelSante
    {
        private int idPharma;

        public Pharmacien(int idPS, string nom, string prenom, int idPharma) : base(idPS, nom, prenom)
        {
            this.IdPharma = idPharma;
        }

        public int IdPharma { get => idPharma; set => idPharma = value; }
    }
}
