// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public sealed partial class InfoAdmin : Page
    {
        public InfoAdmin()
        {
            this.InitializeComponent();
            lvTrajet.ItemsSource = GestionBD.getInstance().getTrajetEnCour();
            //tblMontantChauffeur.Text = GestionBD.getInstance
        }

        private void btRecherche_Click(object sender, RoutedEventArgs e)
        {
            Boolean validation = true;
            string dateDebut;
            string dateFin;
            
            dateDebut = tbxDebut.Date.Date.ToString("yyyy-MM-dd"); 
            dateFin = tbxFin.Date.Date.ToString("yyyy-MM-dd");

            try
            {
                if (dateDebut == "")
                {
                    validation = false;
                    tblAlertDebut.Text = "Vous devez entrer une date de début";
                    tblAlertDebut.Visibility = Visibility.Visible;
                }
                else
                {
                    tblAlertDebut.Visibility = Visibility.Collapsed;                    
                }

                if (dateFin ==  "")
                {
                    validation = false;
                    tblAlertFin.Text = "Vous devez entrer une date de fin";
                    tblAlertFin.Visibility = Visibility.Visible;
                }
                else
                {
                    tblAlertFin.Visibility = Visibility.Collapsed;
                }

                if(validation)
                {
                    tblAlertDebut.Visibility = Visibility.Collapsed;
                    tblAlertFin.Visibility = Visibility.Collapsed;

                    //APPEL DE LA FONCTION DANS LE GESTION BD

                    lvTrajet.ItemsSource = GestionBD.getInstance().getTrajetIntervalle(dateDebut, dateFin);
                }
                
            }
            catch (FormatException ex)
            {
                tblAlertDebut.Text = "Vous devez entrer une date valide";
                tblAlertDebut.Visibility = Visibility.Visible;
            }
        }

        private void btAjoutVille_Click(object sender, RoutedEventArgs e)
        {
            Ville v = new Ville();
            {
                v.Nom = tbxAjoutVille.Text;
            }

            GestionBD.getInstance().ajouterVille(v);

            tblAlertEnr.Text = "Enregistrement réussi!";
            tblAlertEnr.Visibility = Visibility.Visible;
        }

        private void btRechercheTrajetTermine_Click(object sender, RoutedEventArgs e)
        {
            Boolean validation = true;
            string dateRecherche = dpRecherche.Date.Date.ToString("yyyy-MM-dd");

            try
            {
                if(dateRecherche == "")
                {
                    validation = false;
                    tblAlertDate.Text = "Vous devez entrer une date!";
                    tblAlertDate.Visibility = Visibility.Visible;
                }

                if (validation)
                {
                    tblAlertDate.Visibility = Visibility.Collapsed;

                    //APPEL DE LA FONCTION POUR AFFICHER LES TRAJETS TERMINÉS AVEC LA DATE INDIQUÉ

                    lvTrajet.ItemsSource = GestionBD.getInstance().getTrajetTermine(dateRecherche);


                    //AFFICHER LE MONTANT POUR LES CHAUFFEURS


                    //AFFICHER LE MONTANT TOTAL DES GAINS DE LA COMP
                }


                
            }
            catch (FormatException ex)
            {
                tblAlertDate.Text = "Vous devez entrer une dans le format (AAAA-MM-JJ)";
                tblAlertDate.Visibility = Visibility.Visible;
            }
        }
    }
}
