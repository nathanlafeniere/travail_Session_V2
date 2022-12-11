using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    internal class Facture
    {
        int no_facture;
        int no_client;
        int no_chauffeur;
        string date_facturation;
        int montant_facture;
        int dividende;

        public int No_facture { get => no_facture; set => no_facture = value; }
        public int No_client { get => no_client; set => no_client = value; }
        public int No_chauffeur { get => no_chauffeur; set => no_chauffeur = value; }
        public string Date_facturation { get => date_facturation; set => date_facturation = value; }
        public int Montant_facture { get => montant_facture; set => montant_facture = value; }
        public int Dividende { get => dividende; set => dividende = value; }


        public override string ToString()
        {
            return no_facture + " " + no_client + " " + no_chauffeur + " " + date_facturation + " " + montant_facture+ " " + dividende;
        }
    }
}
