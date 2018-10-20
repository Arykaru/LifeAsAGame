using System;
using System.ComponentModel;
using System.Windows.Input;
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


		}

	    private void SongsList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        var con = (string)e.SelectedItem;
	        (viewModel as PlayViewModel).CompositionSelected(con);

	    }
	}
}