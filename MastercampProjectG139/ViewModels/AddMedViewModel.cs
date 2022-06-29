using MastercampProjectG139.Commands;
using MastercampProjectG139.Models;
using MastercampProjectG139.Services;
using MastercampProjectG139.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MastercampProjectG139.ViewModels
{
    class AddMedViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get 
            {
                return _name;
            }
            set 
            { 
                _name = value; 
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _frequence;
        public string Frequence
        {
            get
            {
                return _frequence;
            }
            set
            {
                _frequence = value;
                OnPropertyChanged(nameof(_frequence));
            }
        }

        private string _duration;
        public string Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }
        //Déclaration des méthodes
        public ICommand SubmitCommand { get;}
        public ICommand CancelCommand { get;}
        
        //Le constructeur est appelé lorsqu'on appuie sur le bouton "annuler" ou "ajouter"
        public AddMedViewModel(ModelOrdonnance ordonnance, NavigationService medicamentViewNavigationService)
        {
            SubmitCommand = new AddMedCommand(this, ordonnance, medicamentViewNavigationService);
            CancelCommand = new NavigateCommand(medicamentViewNavigationService); ;
        }
    }
}
