using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace CaronaApp.Universal.Models
{
    public class TakingRideViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        private Geopoint centerPoint;
        private Carona carona;

        public Geopoint CenterPoint
        {
            get { return centerPoint; }
            set
            {
                centerPoint = value;
                NotifyPropertyChanged("CenterPoint");
            }
        }
        
        public Carona Carona
        {
            get { return carona; }
            set
            {
                carona = value;
                NotifyPropertyChanged("Carona");
            }
        }

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
