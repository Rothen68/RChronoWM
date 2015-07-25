using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RChronoWM.Model
{
    public class Sequence
    {
        public string Nom { get; set; }
        public int NbreRepetitions { get; set; }
        private ObservableCollection<Exercice> lstExercices;
        public ObservableCollection<Exercice> LstExercices
        {
            get
            {
                if (lstExercices==null)
                    lstExercices=new ObservableCollection<Exercice>();
                return lstExercices;
            }
            set
            {
                lstExercices=value;
            }

        }

    }
}
