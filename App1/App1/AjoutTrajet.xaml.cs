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
    public sealed partial class AjoutTrajet : Page
    {
        public AjoutTrajet()
        {
            this.InitializeComponent();
        }

        private void nb_ajout_Click(object sender, RoutedEventArgs e)
        {
            Boolean validation = true;

            if (tbxDateTrajet.ToString() == null)
            {
                validation = false;
                tbxDateTrajetE.Text = "Vous devez entrer une date!";
                tbxDateTrajetE.Visibility = Visibility.Visible;
            }

            if (tbxHeureDepart.Text == "")
            {
                validation = false;
                tbxHeureDepartE.Text = "Vous devez entrer un heure";
                tbxHeureDepartE.Visibility = Visibility.Visible;
            }

            if (tbxHeureArriver.Text == "")
            {
                validation = false;
                tbxHeureArriverE.Text = "Vous devez entrer un heure!";
                tbxHeureArriverE.Visibility = Visibility.Visible;
            }

            if (tbxTypeVehicule.Text == "")
            {
                validation = false;
                tbxTypeVehiculeE.Text = "Vous devez entrer un type!";
                tbxTypeVehiculeE.Visibility = Visibility.Visible;
            }



            if (tbxPrixPlace.Value != 15)
            {

                if (tbxPrixPlace.Value != 10)
                {

                    validation = false;
                    tbxPrixPlaceE.Text = "Vous devez entrer un prix valide!";
                    tbxPrixPlaceE.Visibility = Visibility.Visible;
                }
            }
            if (tbxPrixPlace.Value != 10)
            {

                if (tbxPrixPlace.Value != 15)
                {

                    validation = false;
                    tbxPrixPlaceE.Text = "Vous devez entrer un prix valide!";
                    tbxPrixPlaceE.Visibility = Visibility.Visible;
                }
            }

            //QUAND TOUT EST VALIDER ON PEUT CRÉER NOTRE OBJET
            if (validation)
            {
                //METTRE TOUS LES TEXTBLOCKS COLLAPSED
                tbxDateTrajetE.Visibility = Visibility.Collapsed;
                tbxHeureDepartE.Visibility = Visibility.Collapsed;
                tbxHeureArriverE.Visibility = Visibility.Collapsed;
                tbxTypeVehiculeE.Visibility = Visibility.Collapsed;
                tbxNbPlaceE.Visibility = Visibility.Collapsed;
                tbxPrixPlaceE.Visibility = Visibility.Collapsed;

                //CRÉER NOTRE OBJET AVEC TOUS LES CHAMPS

                Trajet trajet = new Trajet();
                {
                    trajet.Date_depart = tbxDateTrajet.Date.Date.ToString("yyyy-MM-dd");
                    trajet.Heure_depart = tbxHeureDepart.Text;
                    trajet.Heure_arrive = tbxHeureArriver.Text;
                    trajet.Type_vehicule = tbxTypeVehicule.Text;
                    trajet.Nb_place = tbxNbPlace.Value;
                    trajet.Prix_place = tbxPrixPlace.Value;
                }

                //APPEL DE LA FONCTION POUR INSÉRER DANS LA BD
                GestionBD.getInstance().ajouterTrajet(trajet);

                //MESSAGE D'ENREGISTREMENT RÉUSSI
                tblAlertValidation.Text = "Enregistrement réussi!";
                tblAlertValidation.Visibility = Visibility.Visible;
            }
        }
    }
}
