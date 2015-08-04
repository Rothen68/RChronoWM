using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RChronoWM.Resources;
using RChronoWM.Model;
using System.Collections.ObjectModel;

namespace RChronoWM.View
{

    public abstract class DataTemplateSelector : ContentControl
    {
        public virtual DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return null;
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            ContentTemplate = SelectTemplate(newContent, this);
        }
    
    }

  

    public class LigneTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BSequence { get; set; }
        public DataTemplate BExercice { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Ligne ligne = item as Ligne;
            if(ligne != null)
            {
                if(ligne.Type == "Sequence")
                {
                    return BSequence;
                }
                else if (ligne.Type == "Exercice")
                {
                    return BExercice;
                }
            }
            return base.SelectTemplate(item, container);
        }
    }


    public partial class MainPage : PhoneApplicationPage
    {

        private List<Ligne> lstLignesChrono;
        // Constructeur
        public MainPage()
        {
            InitializeComponent();

        }

        void lstSequences_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            updateLstViewChrono();
        }

        // Charger les données pour les éléments ViewModel
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.lstSequences.CollectionChanged += lstSequences_CollectionChanged;
            updateLstViewChrono();
        }

        private void updateLstViewChrono()
        {
            Sequence seq;
            Exercice ex;
            int dureeSequence;
            int indexSequence;
            lstLignesChrono = new List<Ligne>();
            for (int i = 0; i < App.lstSequences.Count; i++)
            {
                dureeSequence = 0;

                seq = App.lstSequences[i];
                lstLignesChrono.Add(new Ligne { Nom = seq.Nom, Type = "Sequence", NbreRepetitions = seq.NbreRepetitions });
                indexSequence = lstLignesChrono.Count - 1;
                for (int j = 0; j < seq.LstExercices.Count; j++)
                {
                    ex = seq.LstExercices[j];
                    dureeSequence += ex.Duree;
                    lstLignesChrono.Add(new Ligne { Nom = ex.Nom, Type = "Exercice", Duree = ex.Duree });
                }
                lstLignesChrono[indexSequence].Duree = dureeSequence * seq.NbreRepetitions;
            }
            lstChrono.ItemsSource = lstLignesChrono;
            lstEdit.ItemsSource = lstLignesChrono;

        }

        private void lstEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstEdit.SelectedIndex != -1)
            {
                App.currentSequence = App.getSequenceFromLstEditIndex(lstEdit.SelectedIndex);
                NavigationService.Navigate(new Uri("/View/Edition.xaml", UriKind.Relative));
            }
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            App.lstSequences.CollectionChanged -= lstSequences_CollectionChanged;
        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.lstSequences.Add(new Sequence { Nom = "Nouvelle séquence", NbreRepetitions = 1 } );
        }



        // Exemple de code pour la conception d'une ApplicationBar localisée
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Définit l'ApplicationBar de la page sur une nouvelle instance d'ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Crée un bouton et définit la valeur du texte sur la chaîne localisée issue d'AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Crée un nouvel élément de menu avec la chaîne localisée d'AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}