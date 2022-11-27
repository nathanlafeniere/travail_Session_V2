using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    internal class CompteChauffeur
    {
        string nom;
        string prenom;
        string adresse;
        string telephone;
        string email;
        string no_permis;


        public CompteChauffeur()
        {
            
        }
        public CompteChauffeur(string nom, string prenom, string adresse, string telephone, string email, string no_permis)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.telephone = telephone;
            this.email = email;
            this.no_permis = no_permis;
        }

        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Email { get => email; set => email = value; }
        public string No_permis { get => no_permis; set => no_permis = value; }
    }
}
