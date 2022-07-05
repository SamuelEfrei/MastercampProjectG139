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
using MastercampProjectG139.Commands;
using MastercampProjectG139.Services;
using System.Collections.ObjectModel;
using System.Net.Mail;

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
        private AddMedViewModel amvm;
        private Medecin medecin;
        private string numSS;

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
            DatabaseCommand databaseCommand = new DatabaseCommand();
            numSS = txtBox_numSSPatient.Text;
            databaseCommand.OrdoSubmit(medecin, _ordonnance, numSS);

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.laposte.net");
                mail.From = new MailAddress("ordonline-project@laposte.net");  //définition de l'expediteur du mail
                mail.To.Add(txtBox_mailPatient.Text);  //défintion du destinataire du mail
                mail.Subject = "Votre ordonance en ligne.";
                mail.Body = "Madame, Monsieur,\n\nVeuillez trouver ci-joint votre ordonnance en ligne ainsi que le code à six chiffres à donner à votre phramacien lors du retrait en pharmacie.\n\nCordialement,\n\n\nOrdonline, application d'ordonnances dématérialisées";

                string path = Environment.CurrentDirectory + "\\Ordonnance.pdf";  //selection du path de l'ordonnance

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(path);
                mail.Attachments.Add(attachment);  //ajout de l'ordonnance en pièce jointe

                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("ordonline-project@laposte.net", "Groupe139!!!");
                smtp.EnableSsl = true;
                smtp.Send(mail);  //envoi du mail
                MessageBox.Show("Le mail a bien été envoyé.", "Mail envoyé", MessageBoxButton.OK);  //message affirmant le bon envoi du mail
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ResetFields();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.Show();
            this.Close();
        }

        private AddMedViewModel CreateAddMedViewModel()
        {
            amvm = new AddMedViewModel(_ordonnance, new Services.NavigationService(_navigationStore, CreateMedicamentViewModel));
            return amvm;
        }

        private MedListModel CreateMedicamentViewModel()
        {
            return new MedListModel(_ordonnance, new Services.NavigationService(_navigationStore, CreateAddMedViewModel));
        }

        //Affiche la fenêtre à propos
        private void About_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Topmost = true;
            about.Show();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
        }

        //Reset tous les champs
        private void ResetFields()
        {
            txtBox_mailPatient.Text = "";
            txtBox_numSSPatient.Text = "";
            _ordonnance.RemoveAllMedicaments();
            _navigationStore.CurrentViewModel = CreateMedicamentViewModel();
        }
    }
}
