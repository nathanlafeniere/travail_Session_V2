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
    public sealed partial class InfoClient : Page
    {
        public InfoClient()
        {
            this.InitializeComponent();
            lvTrajetClient.ItemsSource = GestionBD.getInstance().getTrajetClient();  
            
        }

        //BOUTON QUI PERMET D'EMBARQUER DANS UN TRAJET ET RÉDUIRE DE 1 LE NOMBRE DE PLACE DE CE TRAJET
        private void btEmbarquer_Click(object sender, RoutedEventArgs e)
        {
            //METTRE LE BOUTON DÉBARQUER À FALSE
            btDebarquer.IsEnabled = false;

            string test;
            test = lvTrajetClient.SelectedItem.ToString();


            GestionBD.getInstance().getNoTrajet(test);
           

            int id_trajet = GestionBD.getInstance().NoTrajet;

            GestionBD.getInstance().reduireNbPlace(id_trajet);

            tblAlertEmb.Visibility= Visibility.Visible;

            //RENDRE LE BOUTON DISABLE 

            btEmbarquer.IsEnabled = false;
            btDebarquer.IsEnabled = true;
        }

        //BOUTON QUI PERMET DE DÉBARQUER D'UN TRAJET ET D'AUGMENTER DE 1 LE NOMBRE DE PLACE DE CE TRAJET
        private void btDebarquer_Click(object sender, RoutedEventArgs e)
        {
            //DÉCLARATION DE MES VARIABLES
            int numTrajet;
            int numUsager;

            //RENDRE LE BOUTON ENABLED

            

            //FONCTION DOIT LINK LE NO_TRAJET ET LE ID_USER
            //S'ASSURER QUE LE CLIENT NE PEUT PAS DÉBARQUER D'UN AUTRE TRAJET

            numTrajet = GestionBD.getInstance().NumTrajetD;
            numUsager = GestionBD.getInstance().NoUsager;

            tblAlertDebarquer.Text = numTrajet + " " + numUsager;
            tblAlertDebarquer.Visibility = Visibility.Visible;
        }

        

    }
}
