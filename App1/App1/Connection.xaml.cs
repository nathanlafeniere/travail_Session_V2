using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Connection : Page
    {
        public Connection()
        {
            this.InitializeComponent();
        }

        //FONCTION QUI GÈRE LA CONNEXION
        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            //VALIDER SI LE COURRIEL EST VALIDE *******************************************************
            //VALIDER SI LE MOT DE PASSE == LE MOT DE PASSE HASHER DE LA BD ***************************
        }

        //FONCTION QUI GÈRE LES VALIDATIONS ET LA CRÉATION D'UN COMPTE
        private void btCreationCompte_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void btCreerCompte_Click(object sender, RoutedEventArgs e)
        {
            Boolean validation = true;

            if (tbxCreerCourriel.Text == "")
            {
                validation = false;
                tblAlertCreerCourriel.Text = "Vous devez entrer un courriel!";
                tblAlertCreerCourriel.Visibility = Visibility.Visible;
            }


            if (tbxCreerMdp.Text == "")
            {
                validation = false;
                tblAlertCreerMdp.Text = "Vous devez entrer votre mot de passe!";
                tblAlertCreerMdp.Visibility = Visibility.Visible;
            }

            //QUAND LES VALIDATIONS SONT OKAY 

            if (validation)
            {
                //MISE À COLLAPSED DES MESSAGES D'ALERTES
                tblAlertCreerCourriel.Visibility = Visibility.Collapsed;
                tblAlertCreerMdp.Visibility = Visibility.Collapsed;

                //CRÉATION OBJET 

                Usager u = new Usager();
                {
                    u.Email = tbxCreerCourriel.Text;
                    u.Password = tbxCreerMdp.Text;
                }

                //APPEL À LA GESTIONBD ET À LA FONCTION POUR CRÉER ET AJOUTER MON OBJET DANS LA TABLE USAGER
                //GestionBD.getInstance().


                //AFFICHAGE MESSAGE DE RÉUSSITE DE L'ENREGISTREMENT
                tblAlertEnr.Text = "Enregistrement réussi!";
                tblAlertEnr.Visibility = Visibility.Visible;

            }
        }
    }
}
