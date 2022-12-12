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
using static System.Net.Mime.MediaTypeNames;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InfoChauffeur : Page
    {
        public InfoChauffeur()
        {
            this.InitializeComponent();
            listeTrajet.ItemsSource = GestionBD.getInstance().getTrajetsChauffeur(GestionBD.getInstance().No_chauffeur);

        }

       
            
           

        private void btTrajet_Click(object sender, RoutedEventArgs e)
        {
            string test = listeTrajet.SelectedItem.ToString();

            GestionBD.getInstance().getNoTrajet(test);
            lvClient.ItemsSource = GestionBD.getInstance().getClientNomPrenom();
            GestionBD.getInstance().montant_Par_Trajet(GestionBD.getInstance().NoTrajet);
            gain.Text = GestionBD.getInstance().Montant_trajet.ToString();
            dividante.Text = ((GestionBD.getInstance().Montant_trajet) * 0.90).ToString();
        }
    }
}
