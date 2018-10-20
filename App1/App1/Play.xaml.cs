using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Play : ContentPage
	{
		public Play ()
		{
			InitializeComponent ();
		}

	    private void SongsList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        var con = (string)e.SelectedItem;
	        var sd = con;
	    }
	}
}