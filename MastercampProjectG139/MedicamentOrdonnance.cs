using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139
{
    public class MedicamentOrdonnance : Medicament
    {
        private int idOrdo;
        private int idMedic;        
        private string dureeMedicament;
        private string quantiteParJour;

        public MedicamentOrdonnance(int idMedic, int idOrdo, string dureeMedicament, string quantiteParJour, string nom, bool dangereux) 
            : base(idMedic, nom, dangereux)
        {
            this.IdMedic = idMedic;
            this.IdOrdo = idOrdo;
            this.DureeMedicament = dureeMedicament;
            this.QuantiteParJour = quantiteParJour;
        }

        public int IdOrdo { get => idOrdo; set => idOrdo = value; }
        public int IdMedic { get => idMedic; set => idMedic = value; }
        public string DureeMedicament { get => dureeMedicament; set => dureeMedicament = value; }
        public string QuantiteParJour { get => quantiteParJour; set => quantiteParJour = value; }
    }
}
