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

        #region VuePharmacien-init
        public VuePharmacien() => InitializeComponent();

        public VuePharmacien(Pharmacien pharmacien)
        {
            InitializeComponent();
            this.pharmacien = pharmacien;
            txtBlock_nomPrenom.Text = pharmacien.getNom().ToUpper() + " " + pharmacien.getPrenom().ToUpper();
            _ordoP = new ModelOrdonnance("Ordonnance Pharmacien");

        }
        #endregion

        //Création de la liste qui permet d'afficher les médicaments sur l'appli
        public ObservableCollection<ModelMedicament> Medlist()
        {
            //On crée une liste qui chope tous les medocs
            IEnumerable<ModelMedicament> medicaments = _ordoP.GetNonDistributedMedicaments();
            if (medicaments != null) {
                //si jamais elle est pas vide (car déja utilisée auparavant), on la vide et la re-remplie avec les nouveaux médocs 
                medicaments = Enumerable.Empty<ModelMedicament>();
                medicaments = _ordoP.GetNonDistributedMedicaments();
                _medlist = new ObservableCollection<ModelMedicament>(medicaments);
                _ordoP.RemoveAllMedicaments();
                return _medlist;
            }
            else
            {
                //sinon on l'a remplie normalement
                _medlist = new ObservableCollection<ModelMedicament>(medicaments);
                return _medlist;
            }
        }

        #region Buttons
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

        private void btn_getOrdo_Click(object sender, RoutedEventArgs e)
        {
            GetOrdo(false);
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Topmost = true;
            about.Show();
        }

        private void btn_updateOrdo_Click(object sender, RoutedEventArgs e)
        {
            UpdateOrdo();
        }
        #endregion


        private void GetOrdo(bool refresh)
        {
            //On ouvre une connexion à la base de données pour récupérer les donnnées
            DatabaseCommand databaseCommand = new DatabaseCommand();
            if(!refresh)
            {
                numSS = txtBox_numSSPatient.Text;
                code = txtBox_codePatient.Text;

                //On reset les champs
                txtBox_numSSPatient.Text = "";
                txtBox_codePatient.Text = "";
            }
            
            databaseCommand.getOrdonnance(pharmacien, numSS, code, _ordoP);
            // On remplie la liste contenant les medocs
            _medlist = Medlist();
            //On affiche ces beaux médicaments
            pharatio.ItemsSource = _medlist;
            if(!refresh)
                MessageBox.Show("Ordonnance Récupérée", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Met à jour l'ordonnance en actualisant le status de chaque médicament
        private void UpdateOrdo()
        {
            int idMedic = -1;
            int idOrdo = -1;
            CheckBox cb;
            TextBlock tb;
            Config conf = new Config();

            foreach (ModelMedicament med in pharatio.SelectedItems)
            {
                idMedic = med.Id;
                idOrdo = med.IdOrdo;

                String connectionString = conf.DbConnectionString;
                MySqlConnection conn = new MySqlConnection(connectionString);
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();

                    //on met à jour l'ordonnance en indiquant quels médicaments ont été délivrés
                    String query2 = "UPDATE MedicamentOrdonnance SET status=true WHERE idOrdo=@idordo AND idMedic=@idmedic";
                    MySqlCommand cmd2 = new MySqlCommand(query2, conn);
                    cmd2.CommandType = System.Data.CommandType.Text;
                    cmd2.Parameters.AddWithValue("@idordo", idOrdo);
                    cmd2.Parameters.AddWithValue("@idmedic", idMedic);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    med.Status = true;
                }
            }
            //Insérer ici les fonctions pour refresh
            GetOrdo(true);
        }
    }
}
