using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139.Models
{
    internal class ModelMedicament
    {
        public int Id { get; }
        public string Name { get;}
        public string Frequence { get;}

        public string Duration { get;}

    
        public ModelMedicament(int id, string name, string frequence, string duration)
        {
            Id = id;
            Name = name;
            Frequence = frequence;
            Duration = duration;
         
        }
    }
}
