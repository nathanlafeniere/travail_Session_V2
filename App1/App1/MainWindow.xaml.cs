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
                default:
                    break;
            }




        }

       
    }
}
