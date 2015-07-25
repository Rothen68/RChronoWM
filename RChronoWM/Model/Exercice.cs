using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RChronoWM.Model
{
    public class Exercice
    {
        public const int NOTIF_VIBREUR = 0x0001;
        public const int NOTIF_POPUP = 0x0010;

        private int notification;
        public string Nom { get; set; }
        public string Description { get; set; }
        public int Duree { get; set; }
        public int Notification
        {
            get
;
            set
           ;
        }

    }

}
