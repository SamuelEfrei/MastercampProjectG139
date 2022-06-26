using MastercampProjectG139.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }


        public MainViewModel(Ordonnance ordonnance)
        {
            //CurrentViewModel = new MedListModel();
            CurrentViewModel = new AddMedViewModel(ordonnance);
        }
    }
}
