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
    public partial class CreateShoppingListView : ContentPage
    {

        public CreateShoppingListView()
        {
            InitializeComponent();
         
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == "")
            {

            }
        }

        private void AddItemButton_Clicked(object sender, EventArgs e)
        {
            /*
            if (InputItem.Text == "")
            {
                InputItem.BackgroundColor = Color.FromHex("#da6c7b");
            }
            else
            {
                InputItem.BackgroundColor = Color.Transparent;
            }
            */
        }
    }
}