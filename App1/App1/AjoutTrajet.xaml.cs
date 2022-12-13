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
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AjoutTrajet : Page
    {
        public AjoutTrajet()
        {
            this.InitializeComponent();
            cbVilleD.ItemsSource = GestionBD.getInstance().getVille();
            cbVilleA.ItemsSource = GestionBD.getInstance().getVille();
        }
        
        private void nb_ajout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tbxDateTrajetE.Visibility = Visibility.Collapsed;
                tbxHeureDepartE.Visibility = Visibility.Collapsed;
                tbxHeureArriverE.Visibility = Visibility.Collapsed;
                tbxTypeVehiculeE.Visibility = Visibility.Collapsed;
                cbV1E.Visibility = Visibility.Collapsed;
                cbV2E.Visibility = Visibility.Collapsed;
                Boolean validation = true;
           
            if (tbxDateTrajet.Date.ToString() == null)
            {
                validation = false;
                tbxDateTrajetE.Text = "Vous devez entrer une date!";
                tbxDateTrajetE.Visibility = Visibility.Visible;
            }

                string expression = "^[2][0][0-9]{2}";
                if (Regex.IsMatch(tbxDateTrajet.Date.ToString(), expression))
                {

                   

                }
                else
                {
                    validation = false;
                    tbxDateTrajetE.Text = "Vous devez entrer une date valide!";
                    tbxDateTrajetE.Visibility = Visibility.Visible;
                }
            

                if (tbxHeureDepart.ToString() == "")
            {
                validation = false;
                tbxHeureDepartE.Text = "Vous devez entrer un heure";
                tbxHeureDepartE.Visibility = Visibility.Visible;
            }

            if (tbxHeureArriver.ToString() == "")
            {
                validation = false;
                tbxHeureArriverE.Text = "Vous devez entrer un heure!";
                tbxHeureArriverE.Visibility = Visibility.Visible;
            }
           
            if (tbxHeureArriver.Time.Hours < tbxHeureDepart.Time.Hours)
            {
                validation = false;
                tbxHeureArriverE.Text = "Vous devez entrer un heure plus grande que celle de depart!";
                tbxHeureArriverE.Visibility = Visibility.Visible;
            if (tbxHeureArriver.Time.Minutes < tbxHeureDepart.Time.Minutes)
            {
                validation = false;
                tbxHeureArriverE.Text = "Vous devez entrer un heure plus grande que celle de depart!";
                tbxHeureArriverE.Visibility = Visibility.Visible;
            }
            }
                
                if(cbVilleD.SelectedIndex == -1)
                {
                    validation = false;
                    cbV1E.Text = "Donnée invalide";
                    cbV1E.Visibility = Visibility.Visible;
                }
                if (cbVilleA.SelectedIndex == -1)
                {
                    validation = false;
                    cbV2E.Text = "Donnée invalide";
                    cbV2E.Visibility = Visibility.Visible;
                }

               
                    
                
               
            if (tbxTypeVehicule.SelectedItem.ToString() == "Berline")
            {
                GestionBD.getInstance().PrixPlace = 10;
                GestionBD.getInstance().NbPlace = 3;
            }
            if (tbxTypeVehicule.SelectedItem.ToString() == "VUS")
            {
                GestionBD.getInstance().PrixPlace = 15;
                GestionBD.getInstance().NbPlace = 3;
            }
            

            //QUAND TOUT EST VALIDER ON PEUT CRÉER NOTRE OBJET
            if (validation)
            {
                //METTRE TOUS LES TEXTBLOCKS COLLAPSED
                tbxDateTrajetE.Visibility = Visibility.Collapsed;
                tbxHeureDepartE.Visibility = Visibility.Collapsed;
                tbxHeureArriverE.Visibility = Visibility.Collapsed;
                tbxTypeVehiculeE.Visibility = Visibility.Collapsed;
                    // tbxNbPlaceE.Visibility = Visibility.Collapsed;
                    // tbxPrixPlaceE.Visibility = Visibility.Collapsed;

                    //CRÉER NOTRE OBJET AVEC TOUS LES CHAMPS
                    try
                    {
                    Trajet trajet = new Trajet();
                    {
                    trajet.Date_depart = tbxDateTrajet.Date.ToString("yyyy-MM-d");
                    trajet.Heure_depart = tbxHeureDepart.Time.ToString();
                    trajet.Heure_arrive = tbxHeureArriver.Time.ToString();
                           
                                trajet.Ville_depart = cbVilleD.SelectedItem.ToString();
                            
                           
                           
                                trajet.Ville_arrive = cbVilleA.SelectedItem.ToString();
                            
                            
                            
                    trajet.Arret = tbxArret.SelectedItem.ToString();
                    trajet.Type_vehicule = tbxTypeVehicule.SelectedItem.ToString();
                    trajet.Nb_place = GestionBD.getInstance().NbPlace;
                    trajet.Prix_place = GestionBD.getInstance().PrixPlace;
                    }
                        //APPEL DE LA FONCTION POUR INSÉRER DANS LA BD
                      GestionBD.getInstance().ajouterTrajet(trajet);
                    }
                    catch(System.NullReferenceException ex)
                    {
                        tblAlertValidation.Text = "Enregistrement échoué!";
                    }
                

               
                

                //MESSAGE D'ENREGISTREMENT RÉUSSI
                tblAlertValidation.Text = "Enregistrement réussi!";
                tblAlertValidation.Visibility = Visibility.Visible;
            }
            }
            catch(System.NullReferenceException ex)
            {
                tblAlertValidation.Text = "Enregistrement échoué!";
            }

            
        }

       
    }
}
