using MastercampProjectG139.Models;
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
        private readonly Ordonnance _ordonnance;

        public AddMedCommand(AddMedViewModel addMedViewModel, Ordonnance ordonnance)
        {
            _addMedViewModel = addMedViewModel;
            _ordonnance = ordonnance;
            _addMedViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(AddMedViewModel.Medoc))
            {
                OnCanExectedChanged();  
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_addMedViewModel.Medoc) && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            Medicament medicament = new Medicament(_addMedViewModel.Medoc, "o", "o");
            try
            {
            _ordonnance.AddMed(medicament);
             
               
                MessageBox.Show("Le médicament a été ajouté", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Ratio Declined", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
