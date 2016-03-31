using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CaronaApp.Universal.Models
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateRideView : Page
    {
        private Geolocator geolocator;

        public Carona Carona { get; set; } = new Carona();
        public int Slots { get; set; } = 1;

        public CreateRideView()
        {
            this.DataContext = Carona;
            this.InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            CaronaService.CreateCarona(Carona);
            this.Frame.Navigate(typeof(RideScreen));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            geolocator = new Geolocator();
            Carona.Location = (await geolocator.GetGeopositionAsync()).AsGeopoint();
        }
    }
}
