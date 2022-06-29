using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139.Models
{
    internal class ModelMedicament
    {
        public string Name { get;}
        public string Frequence { get;}

        public string Duration { get;}
        public ModelMedicament(string name, string frequence, string duration)
        {
            Name = name;
            Frequence = frequence;
            Duration = duration;
        }
    }
}
