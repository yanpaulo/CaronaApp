using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CaronaApp.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void mapControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Request permission to access location
            var accessStatus = await Geolocator.RequestAccessAsync();

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    // Get cancellation token
                    CancellationTokenSource _cts = new CancellationTokenSource();
                    CancellationToken token = _cts.Token;

                    // If DesiredAccuracy or DesiredAccuracyInMeters are not set (or value is 0), DesiredAccuracy.Default is used.
                    Geolocator geolocator = new Geolocator();

                    // Carry out the operation
                    Geoposition pos = await geolocator.GetGeopositionAsync().AsTask(token);

                    mapControl.Center = new Geopoint(new BasicGeoposition()
                    {
                        Altitude = pos.Coordinate.Altitude.GetValueOrDefault(0),
                        Longitude = pos.Coordinate.Longitude,
                        Latitude = pos.Coordinate.Latitude }
                    );
                    mapControl.MapElements.Add(new MapIcon() { Location = mapControl.Center });

                    break;

                case GeolocationAccessStatus.Denied:
                    await new MessageDialog("Acesso negado").ShowAsync();
                    break;

                case GeolocationAccessStatus.Unspecified:
                    await new MessageDialog("Erro").ShowAsync();
                    break;
            }
        }
    }
}
