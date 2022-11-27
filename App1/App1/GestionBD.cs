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

        int z = 0;

        public GestionBD()
        {
            this.con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2022_420326ri_eq5;Uid=2130666;Pwd=2130666");
            
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
            while (r.Read())
            {
                listeTrajet.Add(new Trajet(r.GetInt32(0),
                    r.GetString(1),
                    r.GetString(2),
                    r.GetString(3),
                    r.GetString(4),
                    r.GetInt32(5),
                    r.GetInt32(6),
                    r.GetInt32(7),
                    r.GetInt32(8)));
            }
            r.Close();
            con.Close();

            return listeTrajet;
        }

        public ObservableCollection<Trajet> getTrajetEnCour()
        {
            ObservableCollection<Trajet> listeTrajet = new ObservableCollection<Trajet>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT   t.no_trajet, t.heure_depart ,t.heure_arrive\r\n      , CASE\r\n          WHEN arret = true THEN 'Arrêt disponible'\r\n          ELSE 'Pas d arrêt'\r\n          END\r\n          , t.type_vehicule , t.nb_place,\r\n       t.no_voiture, t.no_chauffeur, t.prix_place\r\nFROM trajet t  INNER JOIN voiture v on t.no_voiture = v.no_voiture\r\nWHERE v.en_service = TRUE AND curdate() = t.date;";

            con.Open();

            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                listeTrajet.Add(new Trajet(r.GetInt32(0),
                    r.GetString(1),
                    r.GetString(2),
                    r.GetString(3),
                    r.GetString(4),
                    r.GetInt32(5),
                    r.GetInt32(6),
                    r.GetInt32(7),
                    r.GetInt32(8)));
            }

            r.Close();
            con.Close();

            return listeTrajet;
        }

        /*
         * Creation de compte chauffeur/client
         * 
         * 
         * 
         */
        
            public void ajouterChauffeur(CompteChauffeur m)
            {
                int retour;



                try
                {
                    MySqlCommand commande = new MySqlCommand();
                    commande.Connection = con;


                    
                    commande.Parameters.AddWithValue("@nom", m.Nom);
                    commande.Parameters.AddWithValue("@prenom", m.Prenom);
                commande.Parameters.AddWithValue("@adresse", m.Adresse);
                commande.Parameters.AddWithValue("@telephone", m.Telephone);
                commande.Parameters.AddWithValue("@email", m.Email);
                commande.Parameters.AddWithValue("@no_permis", m.No_permis);
                

                commande.CommandText = "insert into chauffeur (nom, prenom,email , addresse,  no_telephone, no_permis) values(@nom, @prenom,@email, @adresse,  @telephone,   @no_permis) ";

                    con.Open();
                    commande.Prepare();
                    retour = commande.ExecuteNonQuery();

                    con.Close();

                }
                catch (MySqlException ex)
                {
                    if (con.State == System.Data.ConnectionState.Open)
                        con.Close();
                }
            }
        
    }
}
