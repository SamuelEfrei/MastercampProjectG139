using MastercampProjectG139.Commands;
using MastercampProjectG139.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MastercampProjectG139.ViewModels
{
    internal class AddMedViewModel : ViewModelBase
    {
        private string _medoc;
        public string Medoc
        {
            get 
            {
                return _medoc;
            }
            set 
            { 
                _medoc = value; 
                OnPropertyChanged(nameof(Medoc));
            }
        }

        public ICommand SubmitCommand { get;}

        public AddMedViewModel(Ordonnance ordonnance)
        {
            SubmitCommand = new AddMedCommand(this, ordonnance);
        }
    }
}
