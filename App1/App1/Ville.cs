using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    internal class Ville
    {
        int no_ville;
        string nom;


        public Ville()
        {

        }

        public Ville(string n)
        {          
            this.nom = n;
        }

        public int No_ville { get => no_ville; set => no_ville = value; }
        public string Nom { get => nom; set => nom = value; }


        public override string ToString()
        {
            return nom;
        }
    }
}
