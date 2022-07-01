using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MastercampProjectG139.Models;
using MastercampProjectG139.Stores;
using MastercampProjectG139.ViewModels;
using MastercampProjectG139.Services;
using System.Collections.ObjectModel;

namespace MastercampProjectG139
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly ModelOrdonnance _ordonnance;
        private readonly MedList medList;
        private readonly NavigationStore _navigationStore;
        private Medecin medecin;

        public MainWindow() => InitializeComponent();
      
        public MainWindow(Medecin medecin)
        {
            InitializeComponent();
            _ordonnance = new ModelOrdonnance("Ordonnance");
            _navigationStore = new NavigationStore();
            _navigationStore.CurrentViewModel = CreateMedicamentViewModel();
            DataContext = new MainViewModel(_navigationStore);

            this.medecin = medecin;
            txtBlock_nomPrenom.Text = medecin.getNom().ToUpper() + " " + medecin.getPrenom().ToUpper();

           

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void pdfratio(object sender, RoutedEventArgs e)
        {
            PDF ratio = new PDF();
            ratio.GeneratePDF(medecin, _ordonnance);
            Application.Current.Shutdown();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.Show();
            this.Close();
        }

        private AddMedViewModel CreateAddMedViewModel()
        {
            return new AddMedViewModel(_ordonnance, new Services.NavigationService(_navigationStore, CreateMedicamentViewModel));
        }

        private MedListModel CreateMedicamentViewModel()
        {
            return new MedListModel(_ordonnance, new Services.NavigationService(_navigationStore, CreateAddMedViewModel));
        }



       




    }
}
