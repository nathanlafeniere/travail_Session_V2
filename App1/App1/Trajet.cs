using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    internal class Trajet
    {
        /*
         * 
         * 
         * 
         */
        int id;
        Time heure_depart;
        Time heure_arrive;
        Boolean arret;
        string type_vehicule;
        int nb_place;
        int no_voiture;
        int no_chauffeur;
        int prix_place;

        public Trajet(int id, Time heure_depart, Time heure_arrive, bool arret, string type_vehicule, int nb_place, int no_voiture, int no_chauffeur, int prix_place)
        {
            this.id = id;
            this.heure_depart = heure_depart;
            this.heure_arrive = heure_arrive;
            this.arret = arret;
            this.type_vehicule = type_vehicule;
            this.nb_place = nb_place;
            this.no_voiture = no_voiture;
            this.no_chauffeur = no_chauffeur;
            this.prix_place = prix_place;
        }

        public int Id { get => id; set => id = value; }
        public Time Heure_depart { get => heure_depart; set => heure_depart = value; }
        public Time Heure_arrive { get => heure_arrive; set => heure_arrive = value; }
        public bool Arret { get => arret; set => arret = value; }
        public string Type_vehicule { get => type_vehicule; set => type_vehicule = value; }
        public int Nb_place { get => nb_place; set => nb_place = value; }
        public int No_voiture { get => no_voiture; set => no_voiture = value; }
        public int No_chauffeur { get => no_chauffeur; set => no_chauffeur = value; }
        public int Prix_place { get => prix_place; set => prix_place = value; }

        public override string ToString()
        {
            // modifier plus tard
            return id + " " + heure_depart + " " + heure_arrive + " " + arret + " " + type_vehicule + " " + nb_place + " " + no_voiture + " " + no_chauffeur + " " + prix_place;
        }
    }
}
