using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using Windows.System;

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
        NavigationViewItem infoAdmin;

        string type;
        int noUsager;
        int reponse;
        int noTrajet;
        int numTrajetD;
        int z = 0;
        int no_chauffeur;
        int no_voiture;
        int prixPlace;
        int nbPlace;
        string dateFacture;

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
        public NavigationViewItem InfoAdmin { get => infoAdmin; set => infoAdmin = value; }
        public Frame Frame { get => frame; set => frame = value; }
        public int NumTrajetD { get => numTrajetD; set => numTrajetD = value; }
        public int No_chauffeur { get => no_chauffeur; set => no_chauffeur = value; }
        public int No_voiture { get => no_voiture; set => no_voiture = value; }
        public int PrixPlace { get => prixPlace; set => prixPlace = value; }
        public int NbPlace { get => nbPlace; set => nbPlace = value; }
        public string DateFacture { get => dateFacture; set => dateFacture = value; }


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
                    r.GetString(6),
                    r.GetString(7),
                    r.GetInt32(8),
                    r.GetInt32(9),
                    r.GetInt32(10)
                  ));
            }
            r.Close();
            con.Close();

            return listeTrajet;
        }
        // Get ville
        public ObservableCollection<Ville> getVille()
        {
            ObservableCollection<Ville> listeVille = new ObservableCollection<Ville>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT nom FROM Ville";

            con.Open();

            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                listeVille.Add(new Ville(r.GetString(0)
                    ));
            }
            r.Close();
            con.Close();

            return listeVille;
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
            commande.CommandText = "SELECT   t.no_trajet, t.dateTrajet ,t.heure_depart ,t.heure_arrive  , t.ville_depart, t.ville_arrive, CASE   WHEN arret = true THEN 'Arrêt disponible'    ELSE 'Pas d arrêt'\r\n           END  , t.type_vehicule , t.nb_place, t.no_chauffeur, t.prix_place FROM trajet t\r\n   WHERE curdate() = t.dateTrajet AND curtime() >= t.heure_depart  AND curtime() <= t.heure_arrive;";

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
                    r.GetString(6),
                    r.GetString(7),
                    r.GetInt32(8),
                    r.GetInt32(10),
                    r.GetInt32(11)));;
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

                string arret = t.Arret;

                commande.Parameters.AddWithValue("@date", t.Date_depart);
                commande.Parameters.AddWithValue("@Heure_depart", t.Heure_depart);
                commande.Parameters.AddWithValue("@Heure_arrive", t.Heure_arrive);
                commande.Parameters.AddWithValue("@villeD", t.Ville_depart);
                commande.Parameters.AddWithValue("@villeA", t.Ville_arrive);
                if (arret == "oui")
                {
                    commande.Parameters.AddWithValue("@arret", true);
                }
                if (arret == "non")
                {
                    commande.Parameters.AddWithValue("@arret", false);
                }
                commande.Parameters.AddWithValue("@Type_vehicule", t.Type_vehicule);
                commande.Parameters.AddWithValue("@nb_place", t.Nb_place);
                commande.Parameters.AddWithValue("@Prix_place", t.Prix_place);
                GestionBD.getInstance().getChauffeurId();
                //GestionBD.getInstance().getVoitureId();
                commande.Parameters.AddWithValue("@no_chauffeur", GestionBD.getInstance().No_chauffeur);
                //commande.Parameters.AddWithValue("@no_voiture", GestionBD.getInstance().No_voiture);

                commande.CommandText = "insert into trajet (dateTrajet , heure_depart, heure_arrive,ville_depart,ville_arrive, arret, type_vehicule, nb_place, no_chauffeur , prix_place) values(@date, @Heure_depart,@Heure_arrive,@villeD, @villeA ,@arret,  @Type_vehicule,  @nb_place, @no_chauffeur , @Prix_place ) ";

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
            GestionBD.getInstance().numTrajetD = i;
            int retour;
            GestionBD.getInstance().historique(GestionBD.getInstance().noUsager, GestionBD.getInstance().numTrajetD);
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

                GestionBD.getInstance().numTrajetD = 0;
            }
            catch(MySqlException ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        //FONCTION DE RECHERCHE ( INTERVALLE ENTRE DATE DÉBUT ET DATE FIN )

        public ObservableCollection<Trajet> getTrajetIntervalle(string dateDebut, string dateArrivee)
        {
            ObservableCollection<Trajet> listeTrajet = new ObservableCollection<Trajet>();

            listeTrajet.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;

            commande.Parameters.AddWithValue("@dateDebut", dateDebut);
            commande.Parameters.AddWithValue("@dateFin", dateArrivee);

            commande.CommandText = "SELECT * FROM Trajet WHERE dateTrajet BETWEEN @dateDebut AND @dateFin";
            

            con.Open();
            commande.Prepare();

            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {

                listeTrajet.Add(new Trajet(
                    r.GetInt32(0),
                    r.GetString(1),
                    r.GetString(2),
                    r.GetString(3),
                    r.GetString(4),
                    r.GetString(5),
                    r.GetString(6),
                    r.GetString(7),
                    r.GetInt32(8),
                    r.GetInt32(9),
                    r.GetInt32(10))); ;
            }

            r.Close();
            con.Close();

            return listeTrajet;
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

        //FONCTION POUR RETOURNER LA DATE DU TRAJET

        public void getDate(int i)
        {
            
            try
            {
                int retour = 0;

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;

                commande.Parameters.AddWithValue("@no_trajet", GestionBD.getInstance().NoTrajet);


                commande.CommandText = "SELECT dateTrajet  FROM trajet WHERE no_trajet = @no_trajet";

                con.Open();
                commande.Prepare();
                MySqlDataReader r = commande.ExecuteReader();


                while (r.Read())
                {
                    GestionBD.getInstance().dateFacture = r.GetString(0);
                }



                con.Close();
            }
            catch (MySqlException ex)
            {

                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }


        //FONCTION POUR AJOUTER DANS LA TABLE FACTURE QUAND UN CLIENT CLIC SUR EMBARQUER

        public void ajouterFacture(Facture f)
        {
            int retour;

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;


                commande.Parameters.AddWithValue("@no_facture", f.No_facture);
                commande.Parameters.AddWithValue("@no_client", f.No_client);
                commande.Parameters.AddWithValue("@no_chauffeur", f.No_chauffeur);
                commande.Parameters.AddWithValue("@date_facture", f.Date_facturation);
                commande.Parameters.AddWithValue("@montant_facture", f.Montant_facture);
                commande.Parameters.AddWithValue("@dividende", f.Dividende);

                
                commande.CommandText = "INSERT INTO Facture (no_facture , no_client, no_chauffeur, date_facturation, montant_facture, dividende) values(@no_facture, @no_client, @no_chauffeur, @date_facture, @montant_facture, @dividende)";

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

        // id_chauffeur
        public ObservableCollection<User> getChauffeurId()
        {

            ObservableCollection<User> listeTrajet = new ObservableCollection<User>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.Parameters.AddWithValue("@value", GestionBD.getInstance().NoUsager);
            commande.CommandText = "Select c.no_chauffeur FROM chauffeur c WHERE id_user = @value";

            con.Open();

            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                GestionBD.getInstance().No_chauffeur = r.GetInt32(0);
           
            }
            
           
            r.Close();
            con.Close();
            return listeTrajet;
        }

        //get voiture id
        /*public observablecollection<user> getvoitureid()
        {

            observablecollection<user> listetrajet = new observablecollection<user>();

            mysqlcommand commande = new mysqlcommand();
            commande.connection = con;
            commande.parameters.addwithvalue("@value", gestionbd.getinstance().no_chauffeur);
            commande.commandtext = "select no_voiture from voiture c where no_chauffeur = @value";

            con.open();

            mysqldatareader r = commande.executereader();
            while (r.read())
            {
            
                gestionbd.getinstance().no_voiture = r.getint32(0);
            }


            r.close();
            con.close();
            return listetrajet;
        }
        */
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

        public ObservableCollection<Trajet> getAdminType(int value)
        {

            ObservableCollection<Trajet> listeTrajet = new ObservableCollection<Trajet>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select COUNT(*) FROM admin WHERE id_user = @no;";
            commande.Parameters.AddWithValue("@no", value);
            con.Open();

            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                GestionBD.getInstance().reponse = r.GetInt32(0);
            }
            if (GestionBD.getInstance().reponse == 1)
            {
                GestionBD.getInstance().type = "admin";
            }
            GestionBD.getInstance().reponse = 0;
            r.Close();
            con.Close();

            return listeTrajet;
        }


        public int getNoTrajet(string u)
        {
            int num_trajet;

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

                num_trajet = GestionBD.getInstance().NoTrajet = r.GetInt32(0);
                return num_trajet;

                con.Close();
            }
            catch (MySqlException ex)
            {

                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

        }

        // function pour l'historique trajet/client
        public void historique(int usager, int trajet)
        {
           
            try
            {
                int retour = 0;

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;

                commande.Parameters.AddWithValue("@usager", usager);
                commande.Parameters.AddWithValue("@trajet", trajet);

                commande.CommandText = "CALL p_historique(@usager, @trajet)";

                con.Open();
                commande.Prepare();
                MySqlDataReader r = commande.ExecuteReader();

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
