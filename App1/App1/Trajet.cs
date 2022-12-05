﻿using Org.BouncyCastle.Asn1.Cms;
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
        string date_depart;
        string heure_depart;
        string heure_arrive;
        string arret;
        string type_vehicule;
        int nb_place;
        int no_voiture;
        int no_chauffeur;
        int prix_place;

        

        public Trajet()
        {
        }

        public Trajet(int v1, string v2, string v3, string v4, string v5, int v6, int v7, int v9 ,int v11)
        {
            this.id = v1;
            this.date_depart = v2;
            this.heure_depart = v3;
            this.heure_arrive = v4;
            this.type_vehicule = v5;
         
            this.nb_place = v6;
            this.no_voiture = v7;
            this.no_chauffeur = v9;
        
            this.prix_place = v11;
        }

        public Trajet(int id, string date_depart, string heure_depart, string heure_arrive, string arret, string type_vehicule, int nb_place, int no_voiture, int no_chauffeur, int prix_place)
        {
            this.id = id;
            this.date_depart = date_depart;
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
        public string Date_depart { get => date_depart; set => date_depart = value; }
        public string Heure_depart { get => heure_depart; set => heure_depart = value; }
        public string Heure_arrive { get => heure_arrive; set => heure_arrive = value; }
        public string Arret { get => arret; set => arret = value; }
        public string Type_vehicule { get => type_vehicule; set => type_vehicule = value; }
        public int Nb_place { get => nb_place; set => nb_place = value; }
        public int No_voiture { get => no_voiture; set => no_voiture = value; }
        public int No_chauffeur { get => no_chauffeur; set => no_chauffeur = value; }
        public int Prix_place { get => prix_place; set => prix_place = value; }

        public override string ToString()
        {
            // modifier plus tard
            return id + " " + date_depart + " " + heure_depart + " " + heure_arrive + " " + arret + " " + type_vehicule + " " + nb_place + " " + no_voiture + " " + no_chauffeur + " " + prix_place;
        }
    }
}
