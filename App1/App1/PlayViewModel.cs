using System;
using Plugin.MediaManager;
using System.ComponentModel;
using System.Windows.Input;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Position = Xamarin.Forms.GoogleMaps.Position;

namespace App1
{
    public class PlayViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ImageTapCommand { get; set; }
        public string CurrentSong = "";
        public Map Map;
        public Pin MePin;
        IGeolocator locator = CrossGeolocator.Current;

        private const string PlayImage =
            "https://cdn4.iconfinder.com/data/icons/media-player-icons/80/Media_player_icons-12-512.png";

        private const string StopImage =
            "http://www.jmkxyy.com/play-pause-stop-icon/play-pause-stop-icon-20.jpg";

        private string imagePath = PlayImage;
        public string ImagePath
        {
            get => imagePath;
            set
            {
                imagePath = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImagePath"));
            }
        }

        private string _compositionName = "Composition name...";
        public string CompositionName
        {
            get => _compositionName;
            set
            {
                _compositionName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CompositionName"));
            }
        }

        public PlayViewModel()
        {
            ImageTapCommand = new Command(CmdTapImage);
        }

        public async void InitLocationSubscription()
        {
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            MoveMeInMap(position.Latitude, position.Longitude);
            locator.PositionChanged += (sender, e) => {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var pos = e.Position;
                    CompositionName = $"{pos.Latitude} AND {pos.Longitude}";
                    MoveMeInMap(pos.Latitude, pos.Longitude);
                });
            };
            await locator.StartListeningAsync(TimeSpan.FromSeconds(1), 1);
        }

        private void CmdTapImage()
        {
            var isPlaying = ImagePath == StopImage;
            ImagePath = isPlaying ? PlayImage : StopImage; 
            if (isPlaying)
            {
                CrossMediaManager.Current.Pause();
            }
            else
            {
                CrossMediaManager.Current.Play(CurrentSong);
            }
        }

        public void CompositionSelected(string compositionName)
        {
            CompositionName = compositionName;
            CurrentSong = "" + CurrentSong;

            CurrentSong = "http://z1.fm//download/21812823";
            PlaySong(CurrentSong);
        }

        private async void PlaySong(string filePath)
        {
            await CrossMediaManager.Current.Play(filePath);
            ImagePath = StopImage;
        }

        private void MoveMeInMap(double latitude, double longitude)
        {
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitude, longitude),
                Distance.FromMeters(50)));

            var mePin = MePin;
            if (mePin == null)
            {
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(latitude, longitude),
                    Label = "You"
                };
                MePin = pin;
                Map.Pins.Add(pin);
            }
            else
            {
                mePin.Position = new Position(latitude, longitude);
            }
        }
    }
}
