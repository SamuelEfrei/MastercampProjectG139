using MastercampProjectG139.Commands;
using MastercampProjectG139.Models;
using MastercampProjectG139.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace MastercampProjectG139
{
    /// <summary>
    /// Interaction logic for VuePharmacien.xaml
    /// </summary>
    public partial class VuePharmacien : Window
    {
        private Pharmacien pharmacien;
        private string numSS;
        private string code;
        private readonly ModelOrdonnance _ordoP;
        public ObservableCollection<ModelMedicament> _medlist;


        public VuePharmacien() => InitializeComponent();

        public VuePharmacien(Pharmacien pharmacien)
        {
            InitializeComponent();
            this.pharmacien = pharmacien;
            txtBlock_nomPrenom.Text = pharmacien.getNom().ToUpper() + " " + pharmacien.getPrenom().ToUpper();
            _ordoP = new ModelOrdonnance("Ordonnance Pharmacien");

        }

        //Création de la liste qui permet d'afficher les médicaments sur l'appli
        public ObservableCollection<ModelMedicament> Medlist()
        {
            //On crée une liste qui chope tous les medocs
            IEnumerable<ModelMedicament> lal = _ordoP.GetAllMedicaments();
            if (lal!=null) {
                //si jamais elle est pas vide (car déja utilisée auparavant), on la vide et la re-remplie avec les nouveaux médocs 
                lal = Enumerable.Empty<ModelMedicament>();
                lal = _ordoP.GetAllMedicaments();
                _medlist = new ObservableCollection<ModelMedicament>(lal);
                _ordoP.RemoveAllMedicaments();
                return _medlist;

            }
            else
            {
                //sinon on l'a remplie normalement
                _medlist = new ObservableCollection<ModelMedicament>(lal);
                return _medlist;
            }
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.Show();
            this.Close();
        }



        private void GetOrdo(object sender, RoutedEventArgs e)
        {
            //On ouvre une connexion à la base de données pour récupérer les donnnées
            DatabaseCommand databaseCommand = new DatabaseCommand();
            numSS = txtBox_numSSPatient.Text;
            code = txtBox_codePatient.Text;
            databaseCommand.getOrdonnance(pharmacien, numSS, code, _ordoP);
            // On remplie la liste contenant les medocs
            _medlist = Medlist();
            //On affiche ces beaux médicaments
            pharatio.ItemsSource = _medlist;
            numSS = "";
            code = "";
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Topmost = true;
            about.Show();
        }

        private void btn_updateOrdo_Click(object sender, RoutedEventArgs e)
        {
            string medName = "";
            string medFreq = ""; //Fréquence du médicament
            string medDur = "";  //Durée du traitement
            CheckBox cb;
            TextBlock tb;

            foreach(ListViewItem med in pharatio.Items)
            {
                cb = (CheckBox)med.FindName("med_checkbox"); //On récupère la checkbox et on vérifie sa valeur
                if(cb.IsChecked == true)
                {
                    //On assigne les différentes valeurs
                    tb = (TextBlock)med.FindName("med_name");
                    medName = tb.Text;

                    tb = (TextBlock)med.FindName("med_freq");
                    medFreq = tb.Text;

                    tb = (TextBlock)med.FindName("med_duration");
                    medDur = tb.Text;
                }
            }

            Config conf = new Config();
            MySqlConnection conn = new MySqlConnection(conf.DbConnectionString);
        }
    }
}
