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

        private int code { get; }

        public int GenerateCode()
        {
            Random rand = new Random();
            int code = rand.Next(1, 999999);
            return code;
        }

        public ModelOrdonnance(string name)
        {
            _medList = new MedList();
            Name = name;
            this.code = GenerateCode();
        }

        public IEnumerable<ModelMedicament> GetAllMedicaments()
        {
            return _medList.GetAllMedicaments();
        }

        public void RemoveAllMedicaments()
        {
            _medList.RemoveAllMedicaments();
        }

        public void AddMed(ModelMedicament medicament)
        {
            _medList.AddMedicament(medicament);
        }

        public int getCode()
        {
            return code;
        }
    }
}
