using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RChronoWM.ViewModel
{
    public class ViewListeSequenceViewModel:INotifyPropertyChanged
    {
        private string nom;
        public string Nom
        {
            get
            {
                return nom;
            }
            set
            {
                NotifyPropertyChanged(ref nom, value);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool NotifyPropertyChanged<T>(ref T variable,T valeur, [CallerMemberName] string propertyName = null )
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            NotifyPropertyChanged(propertyName);
            return true;
        }


    }
}
