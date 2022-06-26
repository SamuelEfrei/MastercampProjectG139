using MastercampProjectG139.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139.ViewModels
{
    internal class MedicamentViewModel: ViewModelBase
    {
        private readonly Medicament _ordonnance;
        public string Name => _ordonnance.Name.ToString();
        public string Frequence => _ordonnance.Frequence.ToString();

        public string Duration => _ordonnance.Duration.ToString();

        public MedicamentViewModel(Medicament ordonnance)
        {
            _ordonnance = ordonnance;
            
        }
    }
}
