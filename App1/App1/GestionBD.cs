﻿using System;
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

            r.Close();
            con.Close();

            return listeTrajet;
        }

        public ObservableCollection<Trajet> getTrajetEnCour()
        {
            ObservableCollection<Trajet> listeTrajet = new ObservableCollection<Trajet>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT t.no_trajet, t.heure_depart, t.heure_arrive, t.arret, t.type_vehicule, t.nb_place,\r\nt.no_voiture, t.no_chauffeur, t.prix_place  FROM trajet t\r\n           INNER JOIN voiture v on t.no_voiture = v.no_voiture WHERE v.en_service = TRUE;";

            con.Open();

            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                listeTrajet.Add(new Trajet(r.GetInt32(0),
                    r.GetString(1),
                    r.GetString(2),
                    r.GetBoolean(3),
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
    }
}
