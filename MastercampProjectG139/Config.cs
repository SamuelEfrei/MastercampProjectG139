
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tommy;

namespace MastercampProjectG139
{
    public class Config
    {
        private string dbServer;
        private string dbUsername;
        private string dbPassword;
        private string dbName;
        private string dbConnectionString;
        private int dbPort;

        public Config()
        {
            string path = Environment.CurrentDirectory + "\\config.toml"; //On charge en mémoire le chemin vers le fichier toml
            using (StreamReader sr = new StreamReader(path))
            {
                //On assigne chaque variable à sa valeur dans le fichier toml
                TomlTable table = TOML.Parse(sr);
                dbServer = table["database"]["server"];
                dbName = table["database"]["name"];
                dbPort = table["database"]["port"];
                dbUsername = table["database"]["uid"];
                dbPassword = table["database"]["password"];
            }

            dbConnectionString = "SERVER=" + DbServer + ";PORT=" + DbPort + ";DATABASE=" + DbName + ";UID=" + DbUsername + ";PASSWORD=" + DbPassword;
        }

        //Getters
        public string DbServer { get { return dbServer; } }
        public string DbUsername { get { return dbUsername; } }
        public string DbPassword { get { return dbPassword; } }
        public string DbName { get { return dbName; } }
        public string DbConnectionString { get { return dbConnectionString; } }
        public int DbPort { get { return dbPort; } }
    }
}
