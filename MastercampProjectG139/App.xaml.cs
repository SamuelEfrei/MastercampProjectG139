using MastercampProjectG139.Models;
using MastercampProjectG139.Services;
using MastercampProjectG139.Stores;
using MastercampProjectG139.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MastercampProjectG139
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Ordonnance _ordonnance;
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _ordonnance = new Ordonnance("Ordonnance");
            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateMedicamentViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

        private AddMedViewModel CreateAddMedViewModel()
        {
            return new AddMedViewModel(_ordonnance, new NavigationService(_navigationStore, CreateMedicamentViewModel)) ;
        }

        private MedListModel CreateMedicamentViewModel()
        {
            return new MedListModel(_ordonnance, new NavigationService(_navigationStore, CreateAddMedViewModel));
        }
    }

}
