using ShoppingTracker.ViewModel;
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
    public partial class HistoryView : ContentPage
    {
        public HistoryView()
        {
            InitializeComponent();

        }
        
        // Update ShoppingHistory displayed
        protected override void OnAppearing()
        {
            var vM = BindingContext as HistoryViewModel;
            if (vM != null) 
            { 
                vM.InitializeShoppingHistory();
            }
        }

    }
}