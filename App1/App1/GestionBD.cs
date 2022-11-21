using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace App1
{
    internal class GestionBD
    {
        MySqlConnection con;
        static GestionBD gestionBD = null;
         


        public GestionBD()
        {
            this.con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2022_420326_eq5;Uid=2130666;Pwd=2130666");
        }

        public static GestionBD getInstance()
        {
            if (gestionBD == null) 
                gestionBD = new GestionBD();

            return gestionBD;
        }

        public ObservableCollection<Trajet> getTrajet()
        {
            ObservableCollection<Trajet> listeTrajet = new ObservableCollection<Trajet>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection= con;
            commande.CommandText = "SELECT * FROM Trajet";

            con.Open();

            MySqlDataReader r = commande.ExecuteReader();

            r.Close();
            con.Close();

            return listeTrajet;
        }
    }
}
