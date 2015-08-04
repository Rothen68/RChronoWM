using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RChronoWM.Model;
using System.Collections.ObjectModel;

namespace RChronoWM
{
    public partial class Edition : PhoneApplicationPage
    {

        private class DataContextEditionSequence
        {
            public string Nom
            {
                get
                {
                    if (App.currentSequence != null)
                        return App.currentSequence.Nom;
                    else
                        return "";
                }
                set
                {
                    if (App.currentSequence != null)
                    {
                        App.currentSequence.Nom = value;
                        App.notifyDataChanged();
                    }
                }
            }
            public int NbreRepetitions
            {
                get
                {
                    if (App.currentSequence != null)
                        return App.currentSequence.NbreRepetitions;
                    else
                        return 0;
                }
                set
                {
                    if (App.currentSequence != null)
                    {
                        App.currentSequence.NbreRepetitions = value;
                        App.notifyDataChanged();
                    }
                }
            }
            

        }
        private class DataContextEditionExercice
        {
            public string NomExercice
            {
                get
                {
                    if (App.currentExercice != null)
                        return App.currentExercice.Nom;
                    else
                        return "";
                }
                set
                {
                    if (App.currentExercice != null)
                    {
                        App.currentExercice.Nom = value;
                        App.notifyDataChanged();
                    }
                }
            }
            public string DescriptionExercice
            {
                get
                {
                    if (App.currentExercice != null)
                        return App.currentExercice.Description;
                    else
                        return "";
                }
                set
                {
                    if (App.currentExercice != null)
                    {
                        App.currentExercice.Description = value;
                        App.notifyDataChanged();
                    }
                }
            }
            public int DureeExercice
            {
                get
                {
                    if (App.currentExercice != null)
                        return App.currentExercice.Duree;
                    else
                        return 0;
                }
                set
                {
                    if (App.currentExercice != null)
                    {
                        App.currentExercice.Duree = value;
                        App.notifyDataChanged();
                    }
                }
            }

            public bool NotifVibreur
            {
                get
                {
                    if (App.currentExercice != null)
                        return (App.currentExercice.Notification & Exercice.NOTIF_VIBREUR) > 0;
                    else
                        return false;
                }
                set
                {
                    if (App.currentExercice != null)
                        if(value)
                            App.currentExercice.Notification=App.currentExercice.Notification|Exercice.NOTIF_VIBREUR;
                        else
                            App.currentExercice.Notification=App.currentExercice.Notification&(Exercice.NOTIF_VIBREUR&0x1111);
                }
            }
            public bool NotifPopup
            {
                get
                {
                    if (App.currentExercice != null)
                        return (App.currentExercice.Notification & Exercice.NOTIF_POPUP) > 0;
                    else
                        return false;
                }
                set
                {
                    if (App.currentExercice != null)
                        if (value)
                            App.currentExercice.Notification = App.currentExercice.Notification | Exercice.NOTIF_POPUP;
                        else
                            App.currentExercice.Notification = App.currentExercice.Notification & (Exercice.NOTIF_POPUP & 0x1111);
                }
            }

        }

        private DataContextEditionSequence dataContextSequence = new DataContextEditionSequence();
        private DataContextEditionExercice dataContextExercice = new DataContextEditionExercice();

        public Edition()
        {
            InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.currentSequence !=null)
            {
                DataContext =dataContextSequence;
                lstExercices.ItemsSource = App.currentSequence.LstExercices;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App.currentSequence = null;
            App.currentExercice = null;
        }

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(App.currentExercice==null)
                panEdition.DefaultItem = panEdition.Items[0];
            else
            {
                if (panEdition.SelectedIndex == 0)
                {

                    App.currentExercice = null;
                    panEdition.DefaultItem = panEdition.Items[0];
                    DataContext = dataContextSequence;
                    lstExercices.ItemsSource = null;
                    lstExercices.ItemsSource = App.currentSequence.LstExercices;

                }
            }
        }

        private void lstExercices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstExercices.SelectedItem != null)
            {
                App.currentExercice = (Exercice)lstExercices.SelectedItem;
                DataContext = null;
                DataContext = dataContextExercice;
                panEdition.DefaultItem = panEdition.Items[1];
            }
        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.currentSequence.LstExercices.Add(new Exercice { Nom = "Nouvel exercice", Duree = 1 });
        }
    }
}