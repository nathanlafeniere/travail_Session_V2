using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    class Usager
    {
        string email;
        string password;

        public Usager() 
        {

        }

        public Usager(string e, string p)        
        {
            email = e;
            password = p;
        }

        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }


        public override string ToString()
        {
            return email + " " + password;
        }
    }
}
