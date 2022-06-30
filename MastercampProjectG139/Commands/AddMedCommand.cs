using MastercampProjectG139.Models;
using MastercampProjectG139.Services;
using MastercampProjectG139.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MastercampProjectG139.Commands
{
    internal class AddMedCommand : CommandBase
    {
        private readonly AddMedViewModel _addMedViewModel;
        private readonly ModelOrdonnance _ordonnance;
        private readonly NavigationService _medicamentViewNavigationService;

        public AddMedCommand(AddMedViewModel addMedViewModel, ModelOrdonnance ordonnance, NavigationService medicamentViewNavigationService)
        {
            _addMedViewModel = addMedViewModel;
            _ordonnance = ordonnance;
            _medicamentViewNavigationService = medicamentViewNavigationService;
            //Update le changement de variable
            _addMedViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(AddMedViewModel.Name) || e.PropertyName == nameof(AddMedViewModel.Frequence) || e.PropertyName == nameof(AddMedViewModel.Duration))
            {
                OnCanExecutedChanged();  
            }
        }
        //Vérifie si le champ est vide pour savoir si le le bouton est valide
        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_addMedViewModel.Name) && !string.IsNullOrEmpty(_addMedViewModel.Duration) && !string.IsNullOrEmpty(_addMedViewModel.Frequence) &&  base.CanExecute(parameter);
        }

        //Commande d'exécution lorsqu'on appuie sur le bouton ajouter dans la vue AddMed
        public override void Execute(object parameter)
        {
            
            ModelMedicament medicament = new ModelMedicament(_addMedViewModel.Name, _addMedViewModel.Frequence, _addMedViewModel.Duration);
            try
            {
                //Le nouveau "medicament" est ajouté à la liste "_ordonnance"
                _ordonnance.AddMed(medicament);
              
                //Supprimer la MessageBox si jamais cela vous gêne durant les tests
                MessageBox.Show("Le médicament a été ajouté", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _medicamentViewNavigationService.Navigate();

            }
            catch (Exception)
            {
                MessageBox.Show("Ratio Declined", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
