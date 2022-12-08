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
    public sealed partial class InfoClient : Page
    {
        public InfoClient()
        {
            this.InitializeComponent();
            lvTrajetClient.ItemsSource = GestionBD.getInstance().getTrajetClient();  
            
        }

        private void btEmbarquer_Click(object sender, RoutedEventArgs e)
        {

            Trajet t = new Trajet(lvTrajetClient.SelectedItem.ToString().Substring(0,1));

            int no_trajet;
            no_trajet = t.Id;

            tbltest.Text = lvTrajetClient.SelectedItem.ToString().Substring(0,2);

        }
    }
}
