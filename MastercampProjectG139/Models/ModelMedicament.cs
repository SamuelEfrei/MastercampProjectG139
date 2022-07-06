using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastercampProjectG139.Models
{
    public class ModelMedicament
    {
        public int Id { get; }
        public int IdOrdo { get; }
        public string Name { get;}
        public string Frequence { get;}
        public string Duration { get;}
        public bool Status { get; set; }

    
        public ModelMedicament(int id, string name, string frequence, string duration, bool status, int idOrdo)
        {
            Id = id;
            Name = name;
            Frequence = frequence;
            Duration = duration;
            Status = status;
            IdOrdo = idOrdo;
        }
    }
}
