using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            this.InitializeComponent();

            GestionBD.getInstance().Frame = mainFrame;
            GestionBD.getInstance().Connexion = Connection;
            GestionBD.getInstance().Accueil = Accueil;
            GestionBD.getInstance().CreationClient = CreationClient;
            GestionBD.getInstance().CreationChauffeur = CreationChauffeur;
            GestionBD.getInstance().CreationTrajet = CreationTrajet;
            GestionBD.getInstance().InfoClient = InfoClient;
            GestionBD.getInstance().InfoChauf = InfoChauf;
            GestionBD.getInstance().Deconnection = Deconnection;
            GestionBD.getInstance().InfoAdmin = InfoAdmin;

            mainFrame.Navigate(typeof(Accueil));
            
            
        }
        



        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            
           
            
            

            
            var item = (NavigationViewItem)args.SelectedItem;
            tblHeader.Text = item.Content.ToString();

            switch (item.Name)
            {
                case "Connection":
                    mainFrame.Navigate(typeof(Connection));
                    break;
                case "Accueil":
                    mainFrame.Navigate(typeof(Accueil));
                    break;
                case "CreationClient":
                    mainFrame.Navigate(typeof(Clients));
                    break;
                case "CreationChauffeur":
                    mainFrame.Navigate(typeof(Chauffeur));
                    break;
                case "CreationTrajet":
                    mainFrame.Navigate(typeof(AjoutTrajet));
                    break;
                case "InfoClient":
                    mainFrame.Navigate(typeof(InfoClient));
                    break;
                case "InfoChauf":
                    mainFrame.Navigate(typeof(InfoChauffeur));
                    break;
                case "InfoAdmin":
                    mainFrame.Navigate(typeof(InfoAdmin));
                    break;
                case "Deconnection":
                    GestionBD.getInstance().Reponse = 0;
                    GestionBD.getInstance().Type = "";
                    GestionBD.getInstance().NoUsager = 0;
                    GestionBD.getInstance().NumTrajetD = 0;

                    GestionBD.getInstance().CreationChauffeur.Visibility = Visibility.Collapsed;
                    GestionBD.getInstance().CreationClient.Visibility = Visibility.Collapsed;
                    GestionBD.getInstance().Deconnection.Visibility = Visibility.Collapsed;
                    GestionBD.getInstance().Connexion.Visibility = Visibility.Visible;
                    GestionBD.getInstance().InfoChauf.Visibility = Visibility.Collapsed;
                    GestionBD.getInstance().CreationTrajet.Visibility = Visibility.Collapsed;
                    GestionBD.getInstance().InfoClient.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }




        }

      
    }
}
