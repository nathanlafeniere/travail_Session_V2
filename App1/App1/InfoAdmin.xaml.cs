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
    public sealed partial class InfoAdmin : Page
    {
        public InfoAdmin()
        {
            this.InitializeComponent();
            lvTrajet.ItemsSource = GestionBD.getInstance().getTrajetEnCour();
        }

        private void btRecherche_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
