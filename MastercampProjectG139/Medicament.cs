using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139
{
    public class Medicament
    {
        private int idMedic;
        private string nom;
        private bool dangereux;

        public Medicament(int idMedic, string nom, bool dangereux)
        {
            this.IdMedic = idMedic;
            this.Nom = nom;
            this.Dangereux = dangereux;
        }

        public int IdMedic { get => idMedic; set => idMedic = value; }
        public string Nom { get => nom; set => nom = value; }
        public bool Dangereux { get => dangereux; set => dangereux = value; }
    }
}
