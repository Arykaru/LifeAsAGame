using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Play : ContentPage
    {
        private object viewModel;
        public Play ()
		{
            InitializeComponent ();
		    BindingContext = new PlayViewModel();
            viewModel = BindingContext;
		    (viewModel as PlayViewModel).Map = map;

		    Device.BeginInvokeOnMainThread(() =>
		    {
		        (viewModel as PlayViewModel).InitLocationSubscription();
            });
            

		    (viewModel as PlayViewModel).PlaySongAsked += delegate(object sender, string s)
		    {
		        Device.BeginInvokeOnMainThread(async () =>
		        {
		            bool result = await DisplayAlert("New friend", "Let's listen music together", "OK", "NO");
		            if (result)
		            {
		                (viewModel as PlayViewModel).CompositionSelected(s);
		            }
		        });
		    };

        }

	    private void SongsList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        var con = (string)e.SelectedItem;
	        (viewModel as PlayViewModel).CompositionSelected(con);
	    }
	}
}