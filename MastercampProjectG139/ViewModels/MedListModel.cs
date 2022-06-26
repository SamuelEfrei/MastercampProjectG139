using MastercampProjectG139.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MastercampProjectG139.ViewModels
{
    internal class MedListModel : ViewModelBase
    {
        private readonly ObservableCollection<MedicamentViewModel>  _medicaments;
        public IEnumerable<MedicamentViewModel> Medicaments => _medicaments;
        //public ICommand AddMedCommand { get; }

        public MedListModel()
        {
            _medicaments = new ObservableCollection<MedicamentViewModel>();
            _medicaments.Add(new MedicamentViewModel(new Medicament("oui", "i", "o")));

        }
    }
}
