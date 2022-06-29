using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139.Models
{
    internal class ModelOrdonnance
    {
        private readonly MedList _medList;
        private string Name { get; }

        public ModelOrdonnance(string name)
        {
            _medList = new MedList();
            Name = name;
        }

        public IEnumerable<ModelMedicament> GetAllMedicaments()
        {
            return _medList.GetAllMedicaments();
        }

        public async Task AddMed(ModelMedicament medicament)
        {
            _medList.AddMedicament(medicament);
        }
    }
}
