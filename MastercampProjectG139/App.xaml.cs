using MastercampProjectG139.Models;
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

        public App()
        {
            _ordonnance = new Ordonnance("Ordonnance");
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_ordonnance)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }

}
