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

namespace MastercampProjectG139
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Ordonnance _ordonnance;
        private readonly NavigationStore _navigationStore;

        public MainWindow()
        {
            InitializeComponent();
            _ordonnance = new Ordonnance("Ordonnance");
            _navigationStore = new NavigationStore();
            _navigationStore.CurrentViewModel = CreateMedicamentViewModel();
            DataContext = new MainViewModel(_navigationStore);

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private AddMedViewModel CreateAddMedViewModel()
        {
            return new AddMedViewModel(_ordonnance, new Services.NavigationService(_navigationStore, CreateMedicamentViewModel));
        }

        private MedListModel CreateMedicamentViewModel()
        {
            return new MedListModel(_ordonnance, new Services.NavigationService(_navigationStore, CreateAddMedViewModel));
        }

        //private void BtnAdd_Click(object sender, RoutedEventArgs e)
        //{


        //    ContentControl contentControl = new ContentControl();

        //  //contentControl.DataContext = new AddMedModel();
        //    contentControl.Content = new AddMedModel();

        //   panelMiddle.Children.Add(contentControl);

        //}




    }
}
