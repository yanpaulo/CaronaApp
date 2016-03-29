using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
        private Geolocator geolocator = new Geolocator();
        private MapIcon centerIcon = new MapIcon();
        

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
                    // You should set MovementThreshold for distance-based tracking
                    // or ReportInterval for periodic-based tracking before adding event
                    // handlers. If none is set, a ReportInterval of 1 second is used
                    // as a default and a position will be returned every 1 second.
                    //
                    // Value of 2000 milliseconds (2 seconds) 
                    // isn't a requirement, it is just an example.
                    geolocator = new Geolocator { ReportInterval = 1000 };
                    mapControl.MapElements.Clear();
                    centerIcon = UpdateLocationData(await geolocator.GetGeopositionAsync());
                    mapControl.MapElements.Add(centerIcon);
                    mapControl.Center = centerIcon.Location;
                    // Subscribe to PositionChanged event to get updated tracking positions
                    geolocator.PositionChanged += OnPositionChanged;

                    // Subscribe to StatusChanged event to get updates of location status changes
                    //geolocator.StatusChanged += OnStatusChanged;
                    
                    
                    break;

                case GeolocationAccessStatus.Denied:
                    await new MessageDialog("Access denied!").ShowAsync();
                    break;

                case GeolocationAccessStatus.Unspecified:
                    await new MessageDialog("Unspecified error!").ShowAsync();
                    break;
            }
        }

        /// <summary>
        /// Event handler for PositionChanged events. It is raised when
        /// a location is available for the tracking session specified.
        /// </summary>
        /// <param name="sender">Geolocator instance</param>
        /// <param name="e">Position data</param>
        async private void OnPositionChanged(Geolocator sender, PositionChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                UpdateLocationData(e.Position);
            });
        }

        private void Timer_Callback(object o)
        {

        }

        private MapIcon UpdateLocationData(Geoposition position)
        {
            Geopoint point = new Geopoint(new BasicGeoposition() { Latitude = position.Coordinate.Latitude, Longitude = position.Coordinate.Longitude });
            centerIcon.Location = point;
            return centerIcon;
        }
    }
}
