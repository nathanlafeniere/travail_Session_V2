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

        Frame frame;
        NavigationViewItem connexion;
        NavigationViewItem accueil;
        NavigationViewItem creationClient;
        NavigationViewItem creationChauffeur;
        NavigationViewItem creationTrajet;
        NavigationViewItem infoClient;
        NavigationViewItem infoChauf;
        NavigationViewItem deconnection;

        string type;
        int noUsager;
        int reponse;
        int noTrajet;

        int z = 0;



        public NavigationViewItem Connexion { get => connexion; set => connexion = value; }
      
        public NavigationViewItem Accueil { get => accueil; set => accueil = value; }
        public NavigationViewItem CreationClient { get => creationClient; set => creationClient = value; }
        public NavigationViewItem CreationChauffeur { get => creationChauffeur; set => creationChauffeur = value; }
        public NavigationViewItem CreationTrajet { get => creationTrajet; set => creationTrajet = value; }
        public NavigationViewItem InfoClient { get => infoClient; set => infoClient = value; }
        public NavigationViewItem InfoChauf { get => infoChauf; set => infoChauf = value; }
        public string Type { get => type; set => type = value; }
        public int NoUsager { get => noUsager; set => noUsager = value; }
        public int Reponse { get => reponse; set => reponse = value; }
        public NavigationViewItem Deconnection { get => deconnection; set => deconnection = value; }
        public int NoTrajet { get => noTrajet; set => noTrajet = value; }
        public Frame Frame { get => frame; set => frame = value; }
        

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

        public ObservableCollection<Trajet> getTrajetsChauffeur(int valeur)
        {
            ObservableCollection<Trajet> listeTrajet = new ObservableCollection<Trajet>();
        
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.Parameters.AddWithValue("@valeur", valeur);
            commande.CommandText = "SELECT t.no_trajet, t.dateTrajet ,t.heure_depart ,t.heure_arrive  ,  t.type_vehicule , t.nb_place, t.no_voiture, t.no_chauffeur, t.prix_place FROM trajet t  INNER JOIN chauffeur c on t.no_chauffeur = c.no_chauffeur   WHERE t.no_chauffeur = @valeur GROUP BY c.no_chauffeur;";
            
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
                    r.GetInt32(8))); ;
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
                commande.Parameters.AddWithValue("@id_user", GestionBD.getInstance().NoUsager);

                commande.CommandText = "INSERT INTO Client (nom, prenom, email, addresse, no_telephone, id_user) VALUES(@nom, @prenom, @email, @adresse, @telephone, @id_user)";

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

        //FONCTION POUR AJOUTER UNE VILLE DANS LA BD

        public void ajouterVille(Ville v)
        {
            int retour;

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;

                commande.Parameters.AddWithValue("@nom", v.Nom);

                commande.CommandText = "INSERT INTO Ville (nom) VALUE(@nom)";

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

        //FONCTION POUR RÉDUIRE LE NOMBRE DE PLACE DANS UN TRAJET

        public void reduireNbPlace(int i)
        {
            int retour;

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;

                commande.Parameters.AddWithValue("@id_trajet", i);

                commande.CommandText = "UPDATE Trajet SET nb_place= nb_place -1 WHERE no_trajet = @id_trajet";

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

        //FONCTION POUR AUGMENTER LE NOMBRE DE PLACE DISPONIBLE POUR UN TRAJET

        public void augmenterNbPlace(int i)
        {
            int retour;

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;

                commande.Parameters.AddWithValue("@id_trajet", i);

                commande.CommandText = "UPDATE Trajet SET nb_place= nb_place +1 WHERE no_trajet = @id_trajet";

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
                commande.Parameters.AddWithValue("@id_user", GestionBD.getInstance().NoUsager);

                commande.CommandText = "insert into chauffeur (nom, prenom,email , addresse,  no_telephone, no_permis, id_user) values(@nom, @prenom,@email, @adresse,  @telephone,   @no_permis, @id_user) ";

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
                           GestionBD.getInstance().NoUsager = r.GetInt32(0);
                            
                        }
                                                                   

                        
                con.Close();
            }
            catch(MySqlException ex) 
            {
                
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

        }

        // Aller chercher le type usager
        public ObservableCollection<Trajet> getChauffeurType(int value)
        {
            
            ObservableCollection <Trajet> listeTrajet = new ObservableCollection<Trajet>();
            
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.Parameters.AddWithValue("@value", value);
            commande.CommandText = "Select COUNT(*) FROM chauffeur WHERE id_user = @value";
           
            con.Open();

            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                GestionBD.getInstance().reponse = r.GetInt32(0);
            }
            if (GestionBD.getInstance().reponse == 1)
            {
                GestionBD.getInstance().type = "chauffeur";
            }
            GestionBD.getInstance().reponse = 0;
            r.Close();
            con.Close();

            return listeTrajet;
        }

        //

        public ObservableCollection<Trajet> getClientType(int value)
        {
        
            ObservableCollection <Trajet> listeTrajet = new ObservableCollection<Trajet>();
            
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select COUNT(*) FROM client WHERE id_user = @no;";
            commande.Parameters.AddWithValue("@no", value);
            con.Open();

            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                GestionBD.getInstance().reponse = r.GetInt32(0);
            }
            if (GestionBD.getInstance().reponse == 1)
            {
                GestionBD.getInstance().type = "client";
            }
            GestionBD.getInstance().reponse = 0;
            r.Close();
            con.Close();

            return listeTrajet;
        }


        public void getNoTrajet(string u)
        {
            
            try
            {
                int retour = 0;

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;

                commande.Parameters.AddWithValue("@nom", u);


                commande.CommandText = "SELECT f_noTrajet(@nom)";

                con.Open();
                commande.Prepare();
                MySqlDataReader r = commande.ExecuteReader();


                while (r.Read())
                {
                    GestionBD.getInstance().NoTrajet = r.GetInt32(0);

                }



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
