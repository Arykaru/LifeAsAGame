using Plugin.MediaManager;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1
{
    public class PlayViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ImageTapCommand { get; set; }
        public string CurrentSong = "";

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
    }
}
