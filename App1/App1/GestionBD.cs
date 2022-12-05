﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;

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
        // INSTANCES TRAJET
        /*
         * GET TRAJET
         * 
         */
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
                    r.GetString(5),
                    r.GetInt32(6),
                    r.GetInt32(7),
                    r.GetInt32(8),
                    r.GetInt32(9)));
            }
            r.Close();
            con.Close();

            return listeTrajet;
        }

        /*
         * GET TRAJET SEULEMENT SI ILS SONT EN COURS
         * 
         */
        public ObservableCollection<Trajet> getTrajetEnCour()
        {
            ObservableCollection<Trajet> listeTrajet = new ObservableCollection<Trajet>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT   t.no_trajet, t.dateTrajet ,t.heure_depart ,t.heure_arrive  ,\r\n       CASE   WHEN arret = true THEN 'Arrêt disponible'    ELSE 'Pas d arrêt'\r\n           END  , t.type_vehicule , t.nb_place,\r\n       t.no_voiture, t.no_chauffeur, t.prix_place FROM trajet t\r\n        INNER JOIN voiture v on t.no_voiture = v.no_voiture  WHERE curdate() = t.dateTrajet AND curtime() >= t.heure_depart  AND curtime() <= t.heure_arrive;";

            con.Open();

            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                listeTrajet.Add(new Trajet(r.GetInt32(0),
                    r.GetString(1),
                    r.GetString(2),
                    r.GetString(3),
                    r.GetString(4),
                    r.GetString(5),
                    r.GetInt32(6),
                    r.GetInt32(7),
                    r.GetInt32(8),
                    r.GetInt32(9)));;
            }

            r.Close();
            con.Close();

            return listeTrajet;
        }

        //FONCTION POUR AFFICHER LES TRAJETS EN COURS AVEC LE NOMBRE DE PLACE > 0
        //LES FORMATS ??? CEUX DE DATAGRIP OU CLASSE TRAJET ??? POUR LA POSITION 7 (TYPE VÉHICULE)
        public ObservableCollection<Trajet> getTrajetClient()
        {
            ObservableCollection<Trajet> listeTrajetClient = new ObservableCollection<Trajet>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT   t.no_trajet, t.dateTrajet ,t.heure_depart ,t.heure_arrive  ,\r\n       CASE   WHEN arret = true THEN 'Arrêt disponible'    ELSE 'Pas d arrêt'\r\n           END  , t.type_vehicule , t.nb_place,\r\n       t.no_voiture, t.no_chauffeur, t.prix_place FROM trajet t\r\n        INNER JOIN voiture v on t.no_voiture = v.no_voiture  WHERE nb_place > 0 AND curdate() = t.dateTrajet AND curtime() >= t.heure_depart  AND curtime() <= t.heure_arrive;";

            con.Open();

            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                listeTrajetClient.Add(new Trajet(
                    r.GetInt32(0),
                    r.GetString(1),
                    r.GetString(2),
                    r.GetString(3),
                    r.GetString(4),
                    r.GetString(5),
                    r.GetInt32(6),
                    r.GetInt32(7),
                    r.GetInt32(8),
                    r.GetInt32(9))); ;
            }

            r.Close();
            con.Close();

            return listeTrajetClient;
        }

        public ObservableCollection<Trajet> getTrajetsChauffeur(string valeur)
        {
            ObservableCollection<Trajet> listeTrajet = new ObservableCollection<Trajet>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT   t.no_trajet, t.dateTrajet ,t.heure_depart ,t.heure_arrive  ,  t.type_vehicule , t.nb_place, t.no_voiture, t.no_chauffeur, t.prix_place\r\nFROM trajet t  INNER JOIN chauffeur c on t.no_chauffeur = c.no_chauffeur   WHERE @valeur = t.no_chauffeur\r\nGROUP BY c.no_chauffeur;";

            con.Open();

            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                listeTrajet.Add(new Trajet(r.GetInt32(0),
                    r.GetString(1),
                    r.GetString(2),
                    r.GetString(3),
                    r.GetString(4),
                    r.GetString(5),
                    r.GetInt32(6),
                    r.GetInt32(7),
                    r.GetInt32(8),
                    r.GetInt32(9))); ;
            }

            r.Close();
            con.Close();

            return listeTrajet;
        }

        /*
        * AJOUTER UN TRAJET
        * 
        */
        public void ajouterTrajet(Trajet t)
        {
            int retour;



            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;



                commande.Parameters.AddWithValue("@date", t.Date_depart);
                commande.Parameters.AddWithValue("@Heure_depart", t.Heure_depart);
                commande.Parameters.AddWithValue("@Heure_arrive", t.Heure_arrive);
                commande.Parameters.AddWithValue("@arret", t.Arret);
                commande.Parameters.AddWithValue("@Type_vehicule", t.Type_vehicule);
                commande.Parameters.AddWithValue("@nb_place", t.Nb_place);
                commande.Parameters.AddWithValue("@Prix_place", t.Prix_place);


                commande.CommandText = "insert into trajet (dateTrajet , heure_depart, heure_arrive, arret, type_vehicule, nb_place, prix_place) values(@date, @Heure_depart,@Heure_arrive, @arret,  @Type_vehicule,   @nb_place, @Prix_place ) ";

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

        // INSTANCES CREATION COMPTE
        // INSTANCES CLIENT
        /*
         * Creation de compte client
         * 
         * 
         * 
         */

        public void ajouterClient(Compte client)
        {
            int retour;

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;

                commande.Parameters.AddWithValue("@nom", client.Nom);
                commande.Parameters.AddWithValue("@prenom", client.Prenom);
                commande.Parameters.AddWithValue("@email", client.Email);
                commande.Parameters.AddWithValue("@adresse", client.Adresse);
                commande.Parameters.AddWithValue("@telephone", client.Telephone);

                commande.CommandText = "INSERT INTO Client (nom, prenom, email, addresse, no_telephone) VALUES(@nom, @prenom, @email, @adresse, @telephone)";

                con.Open();
                commande.Prepare();

                retour = commande.ExecuteNonQuery();

                con.Close();
            }
            catch(MySqlException ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        
        /*
         * Creation de compte chauffeur
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

        public void ajouterUsager(Usager u)
        {
            int retour;



            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;



                commande.Parameters.AddWithValue("@nom", u.Email);
                commande.Parameters.AddWithValue("@mdp", u.Password);
                

                //changer nom pour email
                commande.CommandText = "insert into user (nom ,mdp) values(@nom, @mdp) ";

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


        //FONCTION POUR GÉRER LE LOGIN

        public void verifierInfo(Usager u)
        {
            ObservableCollection<Usager> lvtest= new ObservableCollection<Usager>();
            try
            {
                int retour =0;

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;

                commande.Parameters.AddWithValue("@nom", u.Email);
                commande.Parameters.AddWithValue("@mdp", u.Password);

                commande.CommandText = "SELECT f_mot_de_passe2(@nom, @mdp) AS 'login'";

                con.Open();
                commande.Prepare();
                MySqlDataReader r = commande.ExecuteReader();
                
                  
                        while (r.Read())
                        {
                            lvtest.Add(new Usager(r.GetInt32(0)));
                            
                        }
                                                                   

                        
                con.Close();
            }
            catch(MySqlException ex) 
            {
                
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

        }




    }
}
