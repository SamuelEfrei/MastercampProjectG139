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

        //Retourne tous les médicaments dont le status est FALSE
        public IEnumerable<ModelMedicament> GetNonDistributedMedicaments()
        {
            List<ModelMedicament> _medicamentsNonDistribues = new List<ModelMedicament>();
            foreach(ModelMedicament med in _medicaments)
            {
                if(med.Status == false)
                    _medicamentsNonDistribues.Add(med);
            }
            return _medicamentsNonDistribues;
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
