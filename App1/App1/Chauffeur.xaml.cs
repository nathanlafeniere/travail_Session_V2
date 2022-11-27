// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Chauffeur : Page
    {
        public Chauffeur()
        {
            this.InitializeComponent();
        }

        private void btAjout_Click(object sender, RoutedEventArgs e)
        {
            bool valide = true;

            CompteChauffeur chauffeur = new CompteChauffeur();
            {


                if (nom.Text == "")
                {

                    valide = false;
                    nomE.Visibility = Visibility.Visible;
                }

                if (prenom.Text == "")
                {
                    valide = false;
                    prenomE.Visibility = Visibility.Visible;
                }

                if (email.Text == "")
                {
                    valide = false;
                    emailE.Visibility = Visibility.Visible;
                }
                if (Adresse.Text == "")
                {
                    valide = false;
                    AdresseE.Visibility = Visibility.Visible;
                }
                if (no_permis.Text == "")
                {
                    valide = false;
                    permisE.Visibility = Visibility.Visible;
                }
                if (Telephone.Text == "")
                {
                    valide = false;
                    telE.Visibility = Visibility.Visible;
                }

                chauffeur.Nom = nom.Text;
                chauffeur.Prenom = prenom.Text;
                chauffeur.Email= email.Text;
                chauffeur.Adresse = Adresse.Text;
                chauffeur.No_permis = no_permis.Text;
                chauffeur.Telephone = Telephone.Text;

            };
            if (valide)
            {
                GestionBD.getInstance().ajouterChauffeur(chauffeur);
                fin.Text = "enregistrement réussi";
            }
        }
    }
}
