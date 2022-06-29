using MastercampProjectG139.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139.ViewModels
{
    //Afin d'éviter un problème de mémoire on refait un modelview medicament en prenant modèle sur Medicament
    class MedicamentViewModel: ViewModelBase
    {
        private readonly ModelMedicament _ordonnance;
        public string Name => _ordonnance.Name.ToString();
        public string Frequence => _ordonnance.Frequence.ToString();

        public string Duration => _ordonnance.Duration.ToString();

        public MedicamentViewModel(ModelMedicament ordonnance)
        {
            _ordonnance = ordonnance;
            
        }
    }
}
