using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using Xamarin.Forms.Maps;
using System.Collections.ObjectModel;
using PhotoMapApp.Services.Definitions;
using PhotoMapApp.Models;
using Plugin.Geolocator;
using Xamarin.Forms;
using Android;
using Android.App;
using static Android.Manifest;

namespace PhotoMapApp.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private IPostService _postService;
        private IGeolocationService _geolocationService;
        public Map Map { get; private set; }
        private ObservableCollection<Pin> _pinCollection = new ObservableCollection<Pin>();
        public ObservableCollection<Pin> PinCollection { get { return _pinCollection; } set { _pinCollection = value; OnPropertyChanged(); } }

        public MapPageViewModel(INavigationService navigationService, IPostService postService, IGeolocationService geolocationService) : base(navigationService)
        {
            _postService = postService;
            _geolocationService = geolocationService;

            Map = new Map(
                MapSpan.FromCenterAndRadius(
                new Position(37, -122), Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            
            foreach (Post post in _postService.GetPosts())
            {
                Map.Pins.Add(new Pin() { Position = post.GetPosition(), Type = PinType.Generic, Label = post.Name });
            }
            System.Diagnostics.Debug.WriteLine("TEST");

            UpdateMapCenterAsync();
        }

        public async void UpdateMapCenterAsync() 
        {
            var position = await _geolocationService.GetCurrentPositionAsync();
            this.Map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1)));
        }
    }
}
