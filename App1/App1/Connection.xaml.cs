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

        Usager u = new Usager();
        public Connection()
        {
            this.InitializeComponent();
            
        }


        //FONCTION QUI GÈRE LA CONNEXION
        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            Boolean validation = true;

            if (tbxCourrielUsager.Text == "")
            {
                validation = false;
                tblAlertConnexion.Text = "Les informations de connexion ne sont pas valide!";
                tblAlertConnexion.Visibility = Visibility.Visible;
            }

            if(pwdbxMdp.Password == "")
            {
                validation = false;
                tblAlertConnexion.Text = "Les informations de connexion ne sont pas valide!";
                tblAlertConnexion.Visibility = Visibility.Visible;
            }

            //SI LES VALIDATIONS SONT OKAY !!!

            if(validation) 
            {
                //METTRE LES TBL À COLLAPSED
                tblAlertConnexion.Visibility = Visibility.Collapsed;

                Usager usager = new Usager(tbxCourrielUsager.Text, pwdbxMdp.Password);

                //APPEL À LA FONCTION
                
                GestionBD.getInstance().verifierInfo(usager);


                GestionBD.getInstance().getChauffeurType(GestionBD.getInstance().NoUsager);
                GestionBD.getInstance().getChauffeurId();
                
                GestionBD.getInstance().getClientType(GestionBD.getInstance().NoUsager);
                GestionBD.getInstance().getAdminType(GestionBD.getInstance().NoUsager);

                //MESSAGE DE CONNECTION RÉUSSI
                //tblAlertConnectionValide.Text = "Vous êtes connecté!";
                tblAlertConnectionValide.Text = GestionBD.getInstance().Type;
                tblAlertConnectionValide.Visibility = Visibility.Visible;

                if (GestionBD.getInstance().NoUsager > 0)
                {
                GestionBD.getInstance().Connexion.Visibility = Visibility.Collapsed;
                GestionBD.getInstance().CreationChauffeur.Visibility = Visibility.Visible;
                GestionBD.getInstance().CreationClient.Visibility = Visibility.Visible;
                GestionBD.getInstance().Deconnection.Visibility = Visibility.Visible;
                }
                else
                {
                    GestionBD.getInstance().CreationChauffeur.Visibility = Visibility.Collapsed;
                    GestionBD.getInstance().CreationClient.Visibility = Visibility.Collapsed;
                    GestionBD.getInstance().Deconnection.Visibility = Visibility.Collapsed;
                    GestionBD.getInstance().Connexion.Visibility = Visibility.Visible;
                }
                if (GestionBD.getInstance().Type == "chauffeur")
                {
                    GestionBD.getInstance().InfoChauf.Visibility = Visibility.Visible;
                    GestionBD.getInstance().CreationTrajet.Visibility = Visibility.Visible;
                    GestionBD.getInstance().CreationChauffeur.Visibility = Visibility.Collapsed;
                }
                else
                {
                    GestionBD.getInstance().InfoChauf.Visibility = Visibility.Collapsed;
                    GestionBD.getInstance().CreationTrajet.Visibility = Visibility.Collapsed;
                }
                if (GestionBD.getInstance().Type == "client")
                {
                    GestionBD.getInstance().InfoClient.Visibility = Visibility.Visible;
                    GestionBD.getInstance().CreationClient.Visibility = Visibility.Collapsed;
                }
                else
                {
                    GestionBD.getInstance().InfoClient.Visibility = Visibility.Collapsed;
                }
                if (GestionBD.getInstance().Type == "admin")
                {
                    GestionBD.getInstance().InfoAdmin.Visibility = Visibility.Visible;
                    
                }
                else
                {
                    GestionBD.getInstance().InfoAdmin.Visibility = Visibility.Collapsed;
                }



                GestionBD.getInstance().Frame.Navigate(typeof(Accueil));
            }
            

        }
        catch (NullReferenceException ex)
            {
                tblAlertEnr.Text = "Vous devez faire une sélection dans la liste des trajets!";
               
                tblAlertEnr.Visibility = Visibility.Visible;
            }
        }



        //FONCTION QUI GÈRE LES VALIDATIONS ET LA CRÉATION D'UN COMPTE
        private void btCreationCompte_Click(object sender, RoutedEventArgs e)
        {
            stkpnlDejaCompte.Visibility = Visibility.Collapsed;
            stkpnlCreerCompte.Visibility = Visibility.Visible;   
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


            if (pwdbxCreerMdp.Password == "")
            {
                validation = false;
                tblAlertCreerMdp.Text = "Vous devez entrer votre mot de passe!";
                tblAlertCreerMdp.Visibility = Visibility.Visible;
            }

            //VALIDER SI LES 2 MDP SONT IDENTIQUES
            if(pwdbxCreerMdp.Password != pwdbxMdpConfirmation.Password)
            {
                validation = false;
                tblAlertMdpConfirmation.Text = "Le mot de passe est erroné!";
                tblAlertMdpConfirmation.Visibility = Visibility.Visible;
            }

            //QUAND LES VALIDATIONS SONT OKAY 

            if (validation)
            {
                //MISE À COLLAPSED DES MESSAGES D'ALERTES
                tblAlertCreerCourriel.Visibility = Visibility.Collapsed;
                tblAlertCreerMdp.Visibility = Visibility.Collapsed;
                tblAlertMdpConfirmation.Visibility = Visibility.Collapsed;

                //CRÉATION OBJET 

                Usager u = new Usager();
                {
                    u.Email = tbxCreerCourriel.Text;
                    u.Password = pwdbxCreerMdp.Password;
                }

                //APPEL À LA GESTIONBD ET À LA FONCTION POUR CRÉER ET AJOUTER MON OBJET DANS LA TABLE USAGER
                GestionBD.getInstance().ajouterUsager(u);


                //AFFICHAGE MESSAGE DE RÉUSSITE DE L'ENREGISTREMENT
                tblAlertEnr.Text = "Enregistrement réussi!";
                
                tblAlertEnr.Visibility = Visibility.Visible;

            }
        }

        private void btDejaCompte_Click(object sender, RoutedEventArgs e)
        {
            stkpnlCreerCompte.Visibility = Visibility.Collapsed;
            stkpnlDejaCompte.Visibility = Visibility.Visible;
        }
    }
}
