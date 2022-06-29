using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139.Models
{
    internal class MedList
    {
        private readonly List<ModelMedicament> _medicaments;

        public MedList()
        {
            _medicaments = new List<ModelMedicament>();
        }

        public IEnumerable<ModelMedicament> GetAllMedicaments()
        {
            return _medicaments;
        }

        public void AddMedicament(ModelMedicament medicament)
        {
            _medicaments.Add(medicament);
        }
    }
}
