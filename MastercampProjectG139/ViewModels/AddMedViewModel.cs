using MastercampProjectG139.Commands;
using MastercampProjectG139.Models;
using MastercampProjectG139.Services;
using MastercampProjectG139.Stores;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MastercampProjectG139.ViewModels
{
    class AddMedViewModel : ViewModelBase
    {

        private int _id;
        public int Id 
        { 
            get 
            { 
                return _id; 
            }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
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
                OnPropertyChanged(nameof(Frequence));
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

        private bool _status;
        public bool Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public ObservableCollection<string> MedItems { get; set; }

        public void getMed()
        {
            Config conf = new Config();
            String connectionString = conf.DbConnectionString;
            MySqlConnection connection = new MySqlConnection(connectionString);
            {
                try
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    String query = "Select idMedic, nom from Medicament";
                    MySqlCommand mySqlCmd = new MySqlCommand(query, connection);
                    MySqlDataReader reader = mySqlCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        TestItems.Add((string)reader["nom"]);
                    }
                    reader.Close();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }
        //Déclaration des méthodes
        public ICommand SubmitCommand { get;}
        public ICommand CancelCommand { get;}
        public ObservableCollection<string> TestItems { get; set; }

        //Le constructeur est appelé lorsqu'on appuie sur le bouton "annuler" ou "ajouter"
        public AddMedViewModel(ModelOrdonnance ordonnance, NavigationService medicamentViewNavigationService)
        {
            TestItems = new ObservableCollection<string>();
            getMed();
            SubmitCommand = new AddMedCommand(this, ordonnance, medicamentViewNavigationService);
            CancelCommand = new NavigateCommand(medicamentViewNavigationService); ;
        }
    }
}
