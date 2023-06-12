using ShoppingTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ShoppingTracker.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace ShoppingTracker.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    //[QueryProperty(nameof(JSON), "JSON")]
    public partial class ShoppingView : ContentPage
    {
        /*
        ShoppingItemList ActiveShoppingItemList { get; set; }

        string json;
        public string JSON
        {
            get
            {
                return json;
            }
            set
            {
                json = value;
                json = Uri.UnescapeDataString(value ?? String.Empty);
                ActiveShoppingItemList = JsonConvert.DeserializeObject<ShoppingItemList>(json);
            }
        }
        */
        public ShoppingView()
        {
            InitializeComponent();


        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}