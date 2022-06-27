using MastercampProjectG139.Commands;
using MastercampProjectG139.Models;
using MastercampProjectG139.Services;
using MastercampProjectG139.Stores;
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
        private readonly Ordonnance _ordonnance;
        private readonly ObservableCollection<MedicamentViewModel>  _medicaments;
        public IEnumerable<MedicamentViewModel> Medicaments => _medicaments;
        public ICommand AddMedCommand { get; }
       // public ICommand LoadMedicamentsCommand { get;}
        public MedListModel(Ordonnance ordonnance, NavigationService addMedNavigationService)
        {
            _ordonnance = ordonnance;
            _medicaments = new ObservableCollection<MedicamentViewModel>();

            AddMedCommand = new NavigateCommand(addMedNavigationService);
            _medicaments.Add(new MedicamentViewModel(new Medicament("oui", "i", "o")));
            _medicaments.Add(new MedicamentViewModel(new Medicament("oui", "i", "o")));

            UpdateMedicaments();

        }

        public void UpdateMedicaments()
        {
            //_medicaments.Clear();

            foreach (Medicament medicament in _ordonnance.GetAllMedicaments())
            {
                MedicamentViewModel medicamentViewModel = new MedicamentViewModel(medicament);
                _medicaments.Add(medicamentViewModel);
            }
        }
    }
}
