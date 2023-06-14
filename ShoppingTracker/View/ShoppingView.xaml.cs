using ShoppingTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ShoppingTracker.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Xamarin.Forms.PlatformConfiguration;

namespace ShoppingTracker.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ShoppingView : ContentPage
    {
        public ShoppingView()
        {
            InitializeComponent();


        }

    }
}