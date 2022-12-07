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
    public sealed partial class Clients : Page
    {
        public Clients()
        {
            this.InitializeComponent();
        }

        private void btValidation_Click(object sender, RoutedEventArgs e)
        {
            Boolean validation = true;

            if(tbxNom.Text == "")
            {
                validation = false;
                tblAlertNom.Text = "Vous devez entrer un nom!";
                tblAlertNom.Visibility = Visibility.Visible;
            }

            if(tbxPrenom.Text == "")
            {
                validation = false;
                tblAlertPrenom.Text = "Vous devez entrer un prenom!";
                tblAlertPrenom.Visibility= Visibility.Visible;
            }

            if(tbxEmail.Text == "")
            {
                validation = false;
                tblAlertEmail.Text = "Vous devez entrer un courriel!";
                tblAlertEmail.Visibility= Visibility.Visible;
            }

            if(tbxAdresse.Text == "")
            {
                validation = false;
                tblAlertAdresse.Text = "Vous devez entrer une adresse!";
                tblAlertAdresse.Visibility= Visibility.Visible;
            }

            if(tbxNoTel.Text == "")
            {
                validation = false;
                tblAlertAdresse.Text = "Vous devez entrer un numero de telephone!";
                tblAlertAdresse.Visibility= Visibility.Visible;
            }

            //QUAND TOUT EST VALIDER ON PEUT CRÉER NOTRE OBJET
            if(validation)
            {
                //METTRE TOUS LES TEXTBLOCKS COLLAPSED
                tblAlertNom.Visibility= Visibility.Collapsed;
                tblAlertPrenom.Visibility= Visibility.Collapsed;
                tblAlertEmail.Visibility= Visibility.Collapsed;
                tblAlertAdresse.Visibility= Visibility.Collapsed;
                tblAlertNoTel.Visibility= Visibility.Collapsed;


                //CRÉER NOTRE OBJET AVEC TOUS LES CHAMPS

                Compte client = new Compte();
                {
                    client.Nom = tbxNom.Text;
                    client.Prenom = tbxPrenom.Text;
                    client.Email = tbxEmail.Text;
                    client.Adresse = tbxAdresse.Text;
                    client.Telephone = tbxNoTel.Text;
                }

                //APPEL DE LA FONCTION POUR INSÉRER DANS LA BD
                GestionBD.getInstance().ajouterClient(client);
              
                GestionBD.getInstance().getClientType(MainWindow.noUsager);
                //MESSAGE D'ENREGISTREMENT RÉUSSI
                tblAlertValidation.Text = "Enregistrement réussi!";
                tblAlertValidation.Visibility= Visibility.Visible;
            }





        }
    }
}
