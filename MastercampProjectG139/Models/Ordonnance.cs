using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139.Models
{
    internal class Ordonnance
    {
        private readonly MedList _medList;
        private string Name { get; }

        public Ordonnance(string name)
        {
            _medList = new MedList();
            Name = name;
        }

        public IEnumerable<Medicament> GetAllMedicaments()
        {
            return _medList.GetAllMedicaments();
        }

        public async Task AddMed(Medicament medicament)
        {
            _medList.AddMedicament(medicament);
        }
    }
}
