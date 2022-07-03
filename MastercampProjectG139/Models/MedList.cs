using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139.Models
{
    internal class MedList
    {
        private List<ModelMedicament> _medicaments;

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

        public void RemoveMedicament(ModelMedicament medicament)
        {
            _medicaments.Remove(medicament);
        }

        public void RemoveAllMedicaments()
        {
            _medicaments.Clear();
        }
    }
}
