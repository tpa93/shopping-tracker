using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingTracker.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryDetailsView : ContentView
	{
		public HistoryDetailsView ()
		{
			InitializeComponent ();
		}
	}
}