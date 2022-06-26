using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139.Models
{
    internal class MedList
    {
        private readonly List<Medicament> _medicaments;

        public MedList()
        {
            _medicaments = new List<Medicament>();
        }

        public IEnumerable<Medicament> GetAllMedicaments()
        {
            return _medicaments;
        }

        public void AddMedicament(Medicament medicament)
        {
            _medicaments.Add(medicament);
        }
    }
}
